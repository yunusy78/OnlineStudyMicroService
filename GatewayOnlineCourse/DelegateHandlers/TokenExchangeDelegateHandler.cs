using IdentityModel.Client;

namespace GatewayOnlineCourse.DelegateHandlers;

public class TokenExchangeDelegateHandler : DelegatingHandler
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    
    private string _accessToken;
    
    public TokenExchangeDelegateHandler(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient();
        _configuration = configuration;
    }

    private async Task<string> GetToken(string requestToken)
    {
        if(!string.IsNullOrEmpty(_accessToken))
            return _accessToken;
        
        var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
        {
            Address = _configuration["IdentityServerURL"],
            Policy = new DiscoveryPolicy { RequireHttps = false }
            
        });
        
        if (disco.IsError)
        {
            throw new Exception(disco.Error);
        }

        TokenExchangeTokenRequest tokenRequest = new TokenExchangeTokenRequest()
        {
            Address = disco.TokenEndpoint,
            ClientId = _configuration["ClientId"]!,
            ClientSecret = _configuration["ClientSecret"],
            GrantType = _configuration["TokenGrantType"]!,
            SubjectToken = requestToken,
            SubjectTokenType = "urn:ietf:params:oauth:token-type:access_token",
            Scope = _configuration["TokenScope"]
        };
        
        var tokenResponse = await _httpClient.RequestTokenExchangeTokenAsync(tokenRequest);
        
        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }
        
        _accessToken = tokenResponse.AccessToken;
        
        return _accessToken;


    }
    
    
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var requestToken = request.Headers.Authorization?.Parameter;
        var accessToken = await GetToken(requestToken);
        request.SetBearerToken(accessToken);
        return await base.SendAsync(request, cancellationToken);
    }
    
    
    
}