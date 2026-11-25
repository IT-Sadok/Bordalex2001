using Application.Features.Users.Interfaces;
using Infrastructure.Consts;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class UserRegistrationService(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager) : IUserRegistrationService
{
    public async Task<Guid> RegisterAsync(
        string email,
        string password,
        string? displayName,
        DateOnly? dateOfBirth,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var oldUser = await userManager.FindByEmailAsync(email);
        if (oldUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        if (!await roleManager.RoleExistsAsync(Roles.Client))
        {
            throw new InvalidOperationException($"Required role '{Roles.Client}' does not exist.");
        }

        var newUser = new AppUser
        {
            UserName = email,
            Email = email,
            DisplayName = displayName ?? string.Empty,
            DateOfBirth = dateOfBirth ?? DateOnly.MinValue
        };

        var result = await userManager.CreateAsync(newUser, password);
        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create user: {errors}");
        }

        var addToRoleResult = await userManager.AddToRoleAsync(newUser, Roles.Client);
        if (!addToRoleResult.Succeeded)
        {
            var errors = string.Join("; ", addToRoleResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to assign role to user: {errors}");

        }

        return Guid.Parse(newUser.Id);
    }
}
