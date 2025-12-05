using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.Data;

namespace Infrastructure.Seeders.Interfaces;

public interface IInitialDbSeeder
{
    Task MigrateAndSeedAsync(CancellationToken ct = default);
}
