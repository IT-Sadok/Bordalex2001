using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class RoleManagerWrapper(RoleManager<IdentityRole> roleManager) : IRoleManagerWrapper<IdentityRole>
{
    public Task<bool> RoleExistsAsync(string roleName) =>
        roleManager.RoleExistsAsync(roleName);
}
