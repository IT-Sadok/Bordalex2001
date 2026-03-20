using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Interfaces;

public interface IRoleManagerWrapper<TRole>
    where TRole : class
{
    Task<bool> RoleExistsAsync(string roleName);
    Task<IdentityResult> CreateAsync(string roleName);
}
