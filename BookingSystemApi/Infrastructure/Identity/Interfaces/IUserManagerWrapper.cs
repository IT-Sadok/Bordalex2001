using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Interfaces;

public interface IUserManagerWrapper<TUser> 
    where TUser : class
{
    Task<TUser?> FindByEmailAsync(string email);
    Task<IList<string>> GetRolesAsync(string userId);
    Task<IdentityResult> CreateAsync(TUser user, string password);
    Task<IdentityResult> AddToRoleAsync(TUser user, string role);
}
