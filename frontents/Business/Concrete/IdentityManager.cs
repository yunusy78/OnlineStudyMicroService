using System.Globalization;
using System.Security.Claims;
using System.Text.Json;
using Business.Abstract;
using Business.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using OnlineStudyShared;

namespace Business.Concrete;

public class IdentityManager : IIdentityService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly ClientSettings _clientSettings;
    private readonly ServiceApiSettings _serviceApiSettings;
    
    public IdentityManager(HttpClient httpClient, IHttpContextAccessor contextAccessor, 
        IOptions<ClientSettings> clientSettings, IOptions<ServiceApiSettings> serviceApiSettings)
    {
        _httpClient = httpClient;
        _contextAccessor = contextAccessor;
        _clientSettings = clientSettings.Value;
        _serviceApiSettings = serviceApiSettings.Value;
    }
    
    public async Task<ResponseDto<bool>> SignIn(SignInInput signInInput)
    {
        var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy()
            {
                RequireHttps = false
            }
        });

        if (disco.IsError)
        {
            throw new Exception(disco.Error);
        }
        
        var passwordTokenRequest = new PasswordTokenRequest
        {
            ClientId = _clientSettings.WebClientUser.ClientId,
            ClientSecret = _clientSettings.WebClientUser.ClientSecret,
            UserName = signInInput.Email,
            Password = signInInput.Password,
            Address = disco.TokenEndpoint
        };
        
        var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);
        
        if (token.IsError)
        {
            var responseContent = await token.HttpResponse.Content.ReadAsStringAsync();
            var errorDto = JsonSerializer.Deserialize<ErrorDto>(responseContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return ResponseDto<bool>.Fail(errorDto.Errors, 400);
        }
        
        var userInfoRequest = new UserInfoRequest
        {
            Token = token.AccessToken,
            Address = disco.UserInfoEndpoint
        };
        
        var userInfo = await _httpClient.GetUserInfoAsync(userInfoRequest);
        
        if (userInfo.IsError)
        {
            throw new Exception(userInfo.Error);
        }
        
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authenticationProperties = new AuthenticationProperties();
        authenticationProperties.StoreTokens(new List<AuthenticationToken>()
        {
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken
            },
            
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
            }
        });
        
        authenticationProperties.IsPersistent = signInInput.RememberMe;
        
        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);
        
        return ResponseDto<bool>.Success(200);
        
    }

    public async Task<TokenResponse> GetAccessTokenByRefreshToken()
    {
        var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy()
            {
                RequireHttps = false
            }
        });

        if (disco.IsError)
        {
            throw new Exception(disco.Error);
        }
        
        var refreshToken = await _contextAccessor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
        
        var refreshTokenRequest = new RefreshTokenRequest
        {
            ClientId = _clientSettings.WebClientUser.ClientId,
            ClientSecret = _clientSettings.WebClientUser.ClientSecret,
            RefreshToken = refreshToken!,
            Address = disco.TokenEndpoint
        };
        
        var token = await _httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
        
        if (token.IsError)
        {
            throw new Exception(token.Error);
        }
        
        var authenticationTokens = new List<AuthenticationToken>()
        {
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.AccessToken,
                Value = token.AccessToken
            },
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.RefreshToken,
                Value = token.RefreshToken!
            },
            
            new AuthenticationToken()
            {
                Name = OpenIdConnectParameterNames.ExpiresIn,
                Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString("o", CultureInfo.InvariantCulture)
            }
        };
        
        var authenticationResult = await _contextAccessor.HttpContext.AuthenticateAsync();
        
        authenticationResult.Properties!.StoreTokens(authenticationTokens);
        
        await _contextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, authenticationResult.Principal!, authenticationResult.Properties);
        
        return token;
        
    }

    public async Task RevokeRefreshToken()
    {
        var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
        {
            Address = _serviceApiSettings.IdentityBaseUri,
            Policy = new DiscoveryPolicy()
            {
                RequireHttps = false
            }
        });
        
        var refreshToken = await _contextAccessor.HttpContext!.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);
        
        var refreshTokenRequest = new TokenRevocationRequest
        {
            ClientId = _clientSettings.WebClientUser.ClientId,
            ClientSecret = _clientSettings.WebClientUser.ClientSecret,
            Address = disco.RevocationEndpoint,
            Token = refreshToken!,
            TokenTypeHint = OpenIdConnectParameterNames.RefreshToken
        };
        
        await _httpClient.RevokeTokenAsync(refreshTokenRequest);
        
    }
}