using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class UserAuthenticationService(
    UserManager<AppUser> userManager,
    SignInManager<AppUser> signInManager,
    IJwtTokenGenerator jwtGenerator) : IUserAuthenticationService
{
    public async Task<string> LoginAsync(
        string email,
        string password,
        CancellationToken ct = default)
    {
        ct.ThrowIfCancellationRequested();

        var user = await userManager.FindByEmailAsync(email) ?? throw new InvalidOperationException("Invalid email or password.");

        var signInResult = await signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!signInResult.Succeeded)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }

        var roles = await userManager.GetRolesAsync(user);
        var token = await jwtGenerator.GenerateTokenAsync(Guid.Parse(user.Id), user.Email, roles);

        return token;
    }
}
