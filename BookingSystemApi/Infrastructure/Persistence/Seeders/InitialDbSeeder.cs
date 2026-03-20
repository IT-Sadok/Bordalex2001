using Infrastructure.Consts;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Identity.Interfaces;
using Infrastructure.Persistance.Seeders.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeders;

public class InitialDbSeeder(AppDbContext dbContext, IRoleManagerWrapper<IdentityRole> roleManager, IUserManagerWrapper<IdentityUser> userManager) : IInitialDbSeeder
{
    public async Task MigrateAndSeedAsync(CancellationToken ct = default)
    {
#if DEBUG
        await dbContext.Database.MigrateAsync(ct);
#endif

        var roles = new[]
        {
            Roles.Admin,
            Roles.Host,
            Roles.Client
        };

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(roleName);

                if (!result.Succeeded)
                {
                    throw new Exception($"Failed to create role {roleName}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }

        if (await userManager.FindByEmailAsync("admin@local") == null)
        {
            var user = new AppUser
            {
                UserName = "admin@local",
                Email = "admin@local",
                EmailConfirmed = true
            };

            await userManager.CreateAsync(user, "Admin123!");
            await userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}
