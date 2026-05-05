using Application.Features.Imports.DTOs;

namespace Application.Features.Imports.Interfaces;

public interface IJsonBatchReader
{
    IAsyncEnumerable<List<ImportEnvelopeDto>> ReadBatchesAsync(Stream jsonStream, int batchSize, CancellationToken ct = default);
}
