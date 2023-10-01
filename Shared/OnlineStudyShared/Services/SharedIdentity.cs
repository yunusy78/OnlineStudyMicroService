using Microsoft.AspNetCore.Http;

namespace OnlineStudyShared.Services;

public class SharedIdentity : ISharedIdentity
{
    private IHttpContextAccessor _httpContextAccessor;
    
    public SharedIdentity(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
}