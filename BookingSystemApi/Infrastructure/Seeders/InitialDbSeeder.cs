using Infrastructure.Data;
using Infrastructure.Seeders.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Seeders;

public class InitialDbSeeder(AppDbContext dbContext, RoleManager<IdentityRole> roleManager) : IInitialDbSeeder
{
    public async Task MigrateAndSeedAsync(CancellationToken ct = default)
    {
#if DEBUG
        await dbContext.Database.MigrateAsync(ct);
#endif

        foreach (var role in roleManager.Roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(new IdentityRole(role.Name));
            }
        }
    }
}
