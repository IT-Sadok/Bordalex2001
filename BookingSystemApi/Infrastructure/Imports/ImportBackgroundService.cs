using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Imports;

public class ImportBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        /*while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = scopeFactory.CreateScope();
            var importService = scope.ServiceProvider.GetRequiredService<DataImportService>();
            // Here you would typically check for pending import jobs and process them
            // For demonstration, we will just call the import service directly
            await importService.ProcessImportAsync(Guid.NewGuid(), stoppingToken);
            // Wait for a certain period before checking for new jobs
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }*/
    }
}
