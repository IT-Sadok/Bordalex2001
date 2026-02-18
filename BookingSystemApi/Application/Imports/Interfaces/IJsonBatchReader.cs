using Application.Imports.Models;

namespace Application.Imports.Interfaces;

public interface IJsonBatchReader
{
    IAsyncEnumerable<List<ImportEnvelope>> ReadBatchesAsync(Stream jsonStream, int batchSize, CancellationToken ct = default);
}
