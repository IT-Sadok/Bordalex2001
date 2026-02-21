using Application.Imports.Interfaces;
using Application.Imports.Models.DTOs;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Infrastructure.Imports;

public class JsonBatchReader : IJsonBatchReader
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultBufferSize = 1024 * 64
    };

    public async IAsyncEnumerable<List<ImportEnvelopeDto>> ReadBatchesAsync(Stream jsonStream, int batchSize, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var batch = new List<ImportEnvelopeDto>(batchSize);

        await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<ImportEnvelopeDto>(jsonStream, options, ct))
        {
            if (item is null)
                continue;

            batch.Add(item);

            if (batch.Count >= batchSize)
            {
                yield return batch;
                batch = new List<ImportEnvelopeDto>(batchSize);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }
}
