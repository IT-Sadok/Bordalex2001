using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Commands;
using Infrastructure.Consts.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Users.Handlers;

public class RegisterUserHandler(
    IUserManagerWrapper<AppUser> userManager, 
    IRoleManagerWrapper<IdentityRole> roleManager,
    IRoles roles) : IRequestHandler<RegisterUserCommand, Guid>
{
    //private static readonly Roles? roles;

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

        if (roles != null)
        {
            var configuredRoles = roles.GetRoles();
            foreach (var roleName in configuredRoles)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                { 
                    throw new InvalidOperationException($"Role assignment failed.");
                }

                var addToRoleResult = await userManager.AddToRoleAsync(newUser, roleName);
                if (!addToRoleResult.Succeeded)
                {
                    var errors = string.Join("; ", addToRoleResult.Errors.Select(e => e.Description));
                    throw new InvalidOperationException($"Failed to add user to role '{roleName}': {errors}");
                }
            }
        }

        return Guid.Parse(newUser.Id);
    }
}