using Application.Features.Imports.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ImportJobRepository(AppDbContext dbContext) : IImportJobRepository
{
    public async Task CreateAsync(ImportJob job, CancellationToken ct = default)
    {
        await dbContext.ImportJobs.AddAsync(job, ct);
        await dbContext.SaveChangesAsync(ct);
    }

    public async Task<ImportJob?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await dbContext.ImportJobs.FirstOrDefaultAsync(j => j.Id == id, ct);
    }

    public async Task<IReadOnlyList<ImportJob>> GetPendingAsync(CancellationToken ct = default)
    {
        return await dbContext.ImportJobs.Where(j => j.Status == ImportStatus.Pending).ToListAsync(ct);
    }

    public async Task MarkInProgressAsync(Guid id, CancellationToken ct = default)
    {
        var job = await dbContext.ImportJobs.FirstAsync(j => j.Id == id, ct);

        job.Status = ImportStatus.InProgress;
        job.StartedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task MarkCompletedAsync(Guid id, CancellationToken ct = default)
    {
        var job = await dbContext.ImportJobs.FirstAsync(j => j.Id == id, ct);

        job.Status = ImportStatus.Completed;
        job.CompletedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task MarkFailedAsync(Guid id, string errorMessage, CancellationToken ct = default)
    {
        var job = await dbContext.ImportJobs.FirstAsync(j => j.Id == id, ct);

        job.Status = ImportStatus.Failed;
        job.ErrorMessage = errorMessage;
        job.CompletedAt = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(ct);
    }

    public async Task IncrementProcessedAsync(Guid id, int count, CancellationToken ct = default)
    {
        var job = await dbContext.ImportJobs.FirstAsync(j => j.Id == id, ct);

        job.ProcessedRecords += count;

        await dbContext.SaveChangesAsync(ct);
    }
}
