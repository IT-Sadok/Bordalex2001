using Dapper;
using Infrastructure.Data;
using Infrastructure.Persistance.Seeders.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistance.Seeders;

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

        var bookingsCountQuery = "SELECT COUNT(1) FROM Bookings";
        var bookingsCount = await dbConnection.QueryAsync<int>(bookingsCountQuery);

        if (bookingsCount.First() == 0)
        {
            var insertBookingsSql = 
                @"
                INSERT INTO Bookings (ApartmentId, ClientId, StartDate, EndDate, TotalPrice)
                VALUES (@ApartmentId, @ClientId, @StartDate, @EndDate, @TotalPrice);";

            var bookings = new[] 
            {
                new { ApartmentId = Guid.NewGuid(), ClientId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(10), EndDate = DateTime.UtcNow.AddDays(15), TotalPrice = 375.00m },
                new { ApartmentId = Guid.NewGuid(), ClientId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(20), EndDate = DateTime.UtcNow.AddDays(25), TotalPrice = 600.00m },
                new { ApartmentId = Guid.NewGuid(), ClientId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(30), EndDate = DateTime.UtcNow.AddDays(35), TotalPrice = 450.00m },
                new { ApartmentId = Guid.NewGuid(), ClientId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(40), EndDate = DateTime.UtcNow.AddDays(45), TotalPrice = 1000.00m },
                new { ApartmentId = Guid.NewGuid(), ClientId = Guid.NewGuid(), StartDate = DateTime.UtcNow.AddDays(50), EndDate = DateTime.UtcNow.AddDays(55), TotalPrice = 550.00m }
            };

            await dbConnection.ExecuteAsync(insertBookingsSql, bookings);
        }
    }
}
