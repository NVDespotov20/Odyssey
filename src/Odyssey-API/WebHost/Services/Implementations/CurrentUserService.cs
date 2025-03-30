using System.Security.Claims;
using System.Security.Cryptography;
using WebHost.Services.Contracts;

namespace WebHost.Services.Implementations;

public class CurrentUserService : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string UserId => _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
    public string UserName =>  _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;
    public List<string> Roles => _httpContextAccessor.HttpContext?.User?.Claims?.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value.ToString())?.ToList() ?? new List<string>();
    public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
 }