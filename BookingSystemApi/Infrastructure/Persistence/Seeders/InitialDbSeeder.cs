using Dapper;
using Infrastructure.Consts;
using Infrastructure.Identity;
using Infrastructure.Persistance.Seeders.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Diagnostics;

namespace Infrastructure.Persistance.Seeders;

public class InitialDbSeeder(IDbConnection dbConnection, ILogger<InitialDbSeeder> logger) : IInitialDbSeeder
{
    public async Task SeedRolesAsync(CancellationToken ct = default)
    {
        var roles = new[]
        {
            Roles.Admin,
            Roles.Host,
            Roles.Client
        };

        foreach (var role in roles)
        {
            var roleExists = await dbConnection.ExecuteScalarAsync<bool>(
                "SELECT COUNT(1) FROM \"AspNetRoles\" WHERE \"Name\" = @RoleName",
                new { RoleName = role });

            if (!roleExists)
            {
                var result = await dbConnection.ExecuteAsync(
                    "INSERT INTO \"AspNetRoles\" (\"Id\", \"Name\", \"NormalizedName\") VALUES (@Id, @Name, @NormalizedName)",
                    new
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = role,
                        NormalizedName = role.ToUpper()
                    });

                if (!result.Equals(1))
                {
                    logger.LogError("Failed to seed role: {Role}", role);
                }
                else
                {
                    logger.LogInformation("Seeded role: {Role}", role);
                }
            }
        }


        if (await dbConnection.QueryFirstOrDefaultAsync<string>(
            "SELECT \"Id\" FROM \"AspNetUsers\" WHERE \"Email\" = @Email",
            new { Email = "admin@local" }) == null)
        {
            {
                var passwordHasher = new PasswordHasher<AppUser>();

                var user = new AppUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = "admin@local",
                    DisplayName = "admin",
                    Email = "admin@local",
                    NormalizedEmail = "ADMIN@LOCAL",
                    NormalizedUserName = "ADMIN@LOCAL",
                    EmailConfirmed = true,
                    PasswordHash = passwordHasher.HashPassword(null, "Admin123!"),
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                    MustChangePassword = false
                };

                var result = await dbConnection.ExecuteAsync(
                    "INSERT INTO \"AspNetUsers\" (\"Id\", \"UserName\", \"NormalizedUserName\", \"Email\", \"NormalizedEmail\", \"EmailConfirmed\", \"PasswordHash\", \"PhoneNumberConfirmed\", \"TwoFactorEnabled\", \"LockoutEnabled\", \"AccessFailedCount\", \"MustChangePassword\", \"DisplayName\") VALUES (@Id, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, @PasswordHash, @PhoneNumberConfirmed, @TwoFactorEnabled, @LockoutEnabled, @AccessFailedCount, @MustChangePassword, @DisplayName)",
                    user);

                if (!result.Equals(1))
                {
                    logger.LogError("Failed to seed admin user");
                }
                else
                {
                    logger.LogInformation("Seeded admin user");
                }

                var roleResult = await dbConnection.ExecuteAsync(
                    "INSERT INTO \"AspNetUserRoles\" (\"UserId\", \"RoleId\") VALUES (@UserId, (SELECT \"Id\" FROM \"AspNetRoles\" WHERE \"Name\" = @RoleName))",
                    new { UserId = user.Id, RoleName = Roles.Admin });

                if (!roleResult.Equals(1))
                {
                    logger.LogError("Failed to assign Admin role to admin user");
                }
                else
                {
                    logger.LogInformation("Assigned Admin role to admin user");
                }
            }
        }
    }
}
