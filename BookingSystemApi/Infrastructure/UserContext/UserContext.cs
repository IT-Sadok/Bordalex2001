using Microsoft.AspNetCore.Http;

namespace Infrastructure.UserContext;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = (httpContextAccessor.HttpContext?.User) ?? throw new InvalidOperationException("No HttpContext available");

        if (!user.Identity?.IsAuthenticated ?? true)
        {
            return null;
        }

        var userId = user.FindFirst("sub")?.Value
            ?? throw new InvalidOperationException("User does not have a 'sub' claim");
        var fullName = user.FindFirst("name")?.Value ?? "Unknown";
        var email = user.FindFirst("email")?.Value ?? "Unknown";
        var roles = user.FindAll("role").Select(r => r.Value);

        return new CurrentUser(userId, fullName, email, roles);
    }
}
