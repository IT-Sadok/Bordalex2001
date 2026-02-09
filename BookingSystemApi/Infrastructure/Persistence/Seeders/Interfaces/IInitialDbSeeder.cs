using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Infrastructure.Persistance.Seeders.Interfaces;

public interface IInitialDbSeeder
{
    Task MigrateAndSeedAsync(CancellationToken ct = default);
}
