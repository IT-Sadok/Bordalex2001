using Application.Common;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure;

public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor.HttpContext?.User ?? throw new InvalidOperationException("No HttpContext available");

        if (user.Identity?.IsAuthenticated != true)
        {
            return null;
        }

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("User does not have a NameIdentifier claim");
        var fullName = user.FindFirstValue(ClaimTypes.Name)
            ?? "Unknown";
        var email = user.FindFirstValue(ClaimTypes.Email)
            ?? "Unknown";
        var roles = user.FindAll(ClaimTypes.Role)
            .Select(r => r.Value);

        return new CurrentUser(userId, fullName, email, roles);
    }
}
