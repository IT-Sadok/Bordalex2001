using Application.Imports.Models.DTOs;

namespace Application.Imports.Interfaces;

public interface IJsonBatchReader
{
    IAsyncEnumerable<List<ImportEnvelopeDto>> ReadBatchesAsync(Stream jsonStream, int batchSize, CancellationToken ct = default);
}
