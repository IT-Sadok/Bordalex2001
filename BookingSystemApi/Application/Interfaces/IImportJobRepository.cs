using Domain.Entities;

namespace Application.Interfaces;

public interface IImportJobRepository
{
    Task CreateAsync(ImportJob job, CancellationToken ct = default);
    Task<ImportJob?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<ImportJob>> GetPendingAsync(CancellationToken ct = default);
    Task MarkInProgressAsync(Guid id, CancellationToken ct = default);
    Task MarkCompletedAsync(Guid id, CancellationToken ct = default);
    Task MarkFailedAsync(Guid id, string errorMessage, CancellationToken ct = default);
    Task IncrementProcessedAsync(Guid id, int count, CancellationToken ct = default);
}
