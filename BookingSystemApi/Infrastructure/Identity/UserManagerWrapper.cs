using Infrastructure.Identity.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;

public class UserManagerWrapper(UserManager<AppUser> userManager) : IUserManagerWrapper<AppUser>
{
     public async Task<AppUser?> FindByEmailAsync(string email) =>
        await userManager.FindByEmailAsync(email);

    public async Task<IList<string>> GetRolesAsync(string userId) =>
        await userManager.GetRolesAsync(userManager.Users.First(u => u.Id == userId));

    public async Task<IdentityResult> CreateAsync(AppUser user, string password) =>
        await userManager.CreateAsync(user, password);

    public async Task<IdentityResult> AddToRoleAsync(AppUser user, string role) => 
        await userManager.AddToRoleAsync(user, role);
}
