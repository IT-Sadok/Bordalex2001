using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class AppUser : IdentityUser
{
    public string? ExternalId { get; set; }
    public string? DisplayName { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public bool MustChangePassword { get; set; } = true;
}
