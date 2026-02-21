using Application.Imports.Models.DTOs;

namespace Application.Imports.Interfaces;

public interface IImportBatchProcessor
{
    Task<int> ProcessBatchAsync(Guid jobId, IReadOnlyCollection<ImportEnvelopeDto> batch, CancellationToken ct = default);
}
