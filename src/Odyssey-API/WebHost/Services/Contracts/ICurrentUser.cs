using System.Security.Claims;

namespace WebHost.Services.Contracts;

public interface ICurrentUser
{
    string UserId { get; }
    string UserName { get; }
    List<string> Roles { get; }
    ClaimsPrincipal User { get; }
}