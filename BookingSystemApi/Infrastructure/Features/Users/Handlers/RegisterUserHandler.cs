using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Commands;
using Infrastructure.Consts;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Features.Users.Handlers;

public class RegisterUserHandler(
    IUserManagerWrapper<AppUser> userManager, 
    IRoleManagerWrapper<IdentityRole> roleManager) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> HandleAsync(RegisterUserCommand request, CancellationToken ct = default) 
    { 
        var oldUser = await userManager.FindByEmailAsync(request.Email);
        if (oldUser is not null)
        {
            throw new InvalidOperationException("User with this email already exists.");
        }

        var newUser = new AppUser
        {
            UserName = request.Email,
            Email = request.Email,
            DisplayName = request.DisplayName ?? string.Empty,
            DateOfBirth = request.DateOfBirth ?? DateOnly.MinValue
        };

        var identityResult = await userManager.CreateAsync(newUser, request.Password);
        if (!identityResult.Succeeded)
        {
            var errors = string.Join("; ", identityResult.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create user: {errors}");
        }

        if (await roleManager.RoleExistsAsync(Roles.Client))
        {
            var addToRoleResult = await userManager.AddToRoleAsync(newUser, Roles.Client);
            if (!addToRoleResult.Succeeded)
            {
                var errors = string.Join("; ", addToRoleResult.Errors.Select(e => e.Description));
                throw new InvalidOperationException($"Failed to assign role to user: {errors}");
            }
        };

        return Guid.Parse(newUser.Id);
    }
}