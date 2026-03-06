using Application.Features.Imports.DTOs;
using Application.Features.Imports.Interfaces;
using Domain.Entities.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure.Imports.Services;

public sealed class ImportBackgroundService(IServiceScopeFactory scopeFactory, ILogger<ImportBackgroundService> logger) : BackgroundService
{
    private static readonly JsonSerializerOptions jsonOptions = new() { PropertyNameCaseInsensitive = true };

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Import background service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ProcessNextJobAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error processing import job");
            }
            
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        logger.LogInformation("Import background service stopped");
    }

    private async Task ProcessNextJobAsync(CancellationToken ct)
    {
        using var scope = scopeFactory.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var processor = scope.ServiceProvider.GetRequiredService<IImportBatchProcessor>();

        var job = await dbContext.ImportJobs
            .Where(j => j.Status == ImportStatus.Pending).OrderBy(j => j.CreatedAt).FirstOrDefaultAsync(ct);

        if (job == null)
            return;

        try
        {
            job.Status = ImportStatus.InProgress;
            job.StartedAt = DateTime.UtcNow;
            await dbContext.SaveChangesAsync(ct);

            var envelopes = await LoadBatchAsync(job.FilePath, ct);

            job.TotalRecords = envelopes.Sum(e => e.Apartments.Count);
            await dbContext.SaveChangesAsync(ct);

            logger.LogInformation("Processing import job {JobId}", job.Id);

            var processed = await processor.ProcessBatchAsync(job.Id, envelopes, ct);

            logger.LogInformation("Completed processing import job {JobId}. Processed {ProcessedCount} of {TotalCount} records", job.Id, processed, job.TotalRecords);

            job.ProcessedRecords = processed;
            job.Status = ImportStatus.Completed;
            job.CompletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error processing import job {JobId}", job.Id);

            job.Status = ImportStatus.Failed;
            job.ErrorMessage = ex.Message;
            job.CompletedAt = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(ct);

            throw;
        }
    }

    private static async Task<IReadOnlyCollection<ImportEnvelopeDto>> LoadBatchAsync(string filePath, CancellationToken ct)
    {
        var json = await File.ReadAllTextAsync(filePath, ct);

        var data = JsonSerializer.Deserialize<List<ImportEnvelopeDto>>(json, jsonOptions);

        return data ?? [];
    }
}
