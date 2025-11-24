using Application.Common.Mediator;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Users.Handlers;

public class LoginUserHandler(
    SignInManager<AppUser> signInManager,
    UserManager<AppUser> userManager,
    IJwtTokenGenerator jwtGenerator) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> HandleAsync(LoginUserCommand request, CancellationToken ct = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }
        var signInResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (!signInResult.Succeeded)
        {
            throw new InvalidOperationException("Invalid email or password.");
        }
        var roles = await userManager.GetRolesAsync(user);
        var token = await jwtGenerator.GenerateTokenAsync(Guid.Parse(user.Id), user.UserName, roles);
        return token;
    }
}