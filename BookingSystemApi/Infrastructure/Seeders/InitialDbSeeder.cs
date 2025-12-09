using Dapper;
using Infrastructure.Data;
using Infrastructure.Seeders.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Seeders;

public class InitialDbSeeder(AppDbContext dbContext, RoleManager<IdentityRole> roleManager, IDbConnection dbConnection) : IInitialDbSeeder
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

        var apartmentsCountQuery = "SELECT COUNT(1) FROM Apartments";
        var apartmentsCount = await dbConnection.QueryAsync<int>(apartmentsCountQuery);

        if (apartmentsCount.First() == 0)
        {
            var insertApartmentsSql = 
                @"
                INSERT INTO Apartments (HostId, Title, Description, Address, PricePerNight, IsAvailable)
                VALUES (@HostId, @Title, @Description, @Address, @PricePerNight, @IsAvailable);";

            var apartments = new[] 
            {
                new { HostId = Guid.NewGuid(), Title = "Cozy Downtown Apartment", Description = "A cozy apartment in the heart of the city.", Address = "123 Main St, Cityville", PricePerNight = 75.00m, IsAvailable = true },
                new { HostId = Guid.NewGuid(), Title = "Beachside Bungalow", Description = "A beautiful bungalow by the beach.", Address = "456 Ocean Ave, Beachtown", PricePerNight = 120.00m, IsAvailable = true },
                new { HostId = Guid.NewGuid(), Title = "Mountain Cabin Retreat", Description = "A peaceful cabin in the mountains.", Address = "789 Pine Rd, Mountaintown", PricePerNight = 90.00m, IsAvailable = true },
                new { HostId = Guid.NewGuid(), Title = "Luxury City Loft", Description = "A luxurious loft in the city center.", Address = "101 Center St, Metropolis", PricePerNight = 200.00m, IsAvailable = true },
                new { HostId = Guid.NewGuid(), Title = "Suburban Family Home", Description = "A spacious home perfect for families.", Address = "202 Maple Dr, Suburbia", PricePerNight = 110.00m, IsAvailable = true }
            };

            await dbConnection.ExecuteAsync(insertApartmentsSql, apartments);
        }
    }
}
