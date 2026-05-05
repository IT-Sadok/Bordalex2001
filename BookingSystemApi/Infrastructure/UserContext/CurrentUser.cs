namespace Infrastructure.UserContext;

public record CurrentUser(string Id, string FullName, string Email, IEnumerable<string> Roles)
{
    public bool IsInRole(string role) => Roles.Contains(role);
}
