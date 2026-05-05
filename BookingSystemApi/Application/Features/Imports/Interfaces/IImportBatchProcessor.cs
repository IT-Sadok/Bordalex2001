using Application.Features.Imports.DTOs;

namespace Application.Features.Imports.Interfaces;

public interface IImportBatchProcessor
{
    Task<int> ProcessBatchAsync(Guid jobId, IReadOnlyCollection<ImportEnvelopeDto> batch, CancellationToken ct = default);
}
