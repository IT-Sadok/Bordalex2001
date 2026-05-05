namespace Infrastructure.Persistence.Seeders.Interfaces;

public interface IInitialDbSeeder
{
    Task SeedRolesAsync(CancellationToken ct = default);
}
