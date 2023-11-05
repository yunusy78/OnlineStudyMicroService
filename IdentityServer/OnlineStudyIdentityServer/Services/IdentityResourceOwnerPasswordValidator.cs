using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using OnlineStudyIdentityServer.Models;

namespace OnlineStudyIdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
{
    var user = _userManager.FindByEmailAsync(context.UserName).Result;
    if (user == null)
    {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User not found");
        return Task.CompletedTask;
    }

    var passwordCheck = _userManager.CheckPasswordAsync(user, context.Password).Result;
    if (!passwordCheck)
    {
        context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "UserName or password is incorrect");
        return Task.CompletedTask;
    }

    // Kullanıcının rollerini almak için _userManager.GetRolesAsync() kullanın
    var userRoles = _userManager.GetRolesAsync(user).Result;

    List<Claim> claims = new List<Claim>
    {
        new Claim(JwtClaimTypes.Subject, user.Id),
        new Claim(JwtClaimTypes.Name, user.UserName),
        new Claim(JwtClaimTypes.Email, user.Email),
        // Diğer JWT iddiaları
    };

    // Kullanıcının rollerini JWT içine ekleyin
    if (userRoles.Any())
    {
        claims.AddRange(userRoles.Select(role => new Claim(JwtClaimTypes.Role, role)));
    }
    
    context.Result = new GrantValidationResult(user.Id.ToString(), OidcConstants.AuthenticationMethods.Password, claims);
    return Task.CompletedTask;
}

    }
}