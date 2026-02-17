using Domain.Entities;

namespace Application.Interfaces;

public interface IImportJobRepository
{
    Task CreateAsync(ImportJob job, CancellationToken ct);
    Task<ImportJob?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<ImportJob>> GetPendingAsync(CancellationToken ct);
    Task MarkInProgressAsync(Guid id, CancellationToken ct);
    Task MarkCompletedAsync(Guid id, CancellationToken ct);
    Task MarkFailedAsync(Guid id, string errorMessage, CancellationToken ct);
    Task IncrementProcessedAsync(Guid id, int count, CancellationToken ct);
}
