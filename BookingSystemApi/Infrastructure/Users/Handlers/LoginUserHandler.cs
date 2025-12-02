using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Commands;
using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Users.Handlers;

public class LoginUserHandler(
    ISignInManagerWrapper<AppUser> signInManager,
    IUserManagerWrapper<AppUser> userManager,
    IJwtTokenGenerator jwtGenerator) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> HandleAsync(LoginUserCommand request, CancellationToken ct = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email) ?? throw new InvalidOperationException("Invalid email or password.");

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