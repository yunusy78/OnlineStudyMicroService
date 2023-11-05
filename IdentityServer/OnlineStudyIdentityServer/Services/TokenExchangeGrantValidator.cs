using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace OnlineStudyIdentityServer.Services
{
    public class TokenExchangeGrantValidator : IExtensionGrantValidator
    {
        public string GrantType => "urn:ietf:params:oauth:grant-type:token-exchange";
        
        private readonly ITokenValidator _tokenValidator;
        
        public TokenExchangeGrantValidator(ITokenValidator tokenValidator)
        {
            _tokenValidator = tokenValidator;
        }
        
        public Task ValidateAsync(ExtensionGrantValidationContext context)
        {
            var request = context.Request.Raw.ToString();
            var token = context.Request.Raw.Get("subject_token");
            if (string.IsNullOrEmpty(token))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "token missing");
                return Task.CompletedTask;
                
            }
            var tokenResult = _tokenValidator.ValidateAccessTokenAsync(token);
            
            if(tokenResult.Result.IsError)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "token invalid");
                return Task.CompletedTask;
            }
            
            var subjectClaim = tokenResult.Result.Claims.FirstOrDefault(x => x.Type == "sub");
            if (subjectClaim == null)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "token must contain sub value");
                return Task.CompletedTask;
            }
            
            context.Result = new GrantValidationResult(subjectClaim.Value, "access_token", tokenResult.Result.Claims);
            return Task.CompletedTask;
        }

       
    }
}