using Application.Imports.Interfaces;
using Application.Imports.Models;
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

    public async IAsyncEnumerable<List<ImportEnvelope>> ReadBatchesAsync(Stream jsonStream, int batchSize, [EnumeratorCancellation] CancellationToken ct = default)
    {
        var batch = new List<ImportEnvelope>(batchSize);

        await foreach (var item in JsonSerializer.DeserializeAsyncEnumerable<ImportEnvelope>(jsonStream, options, ct))
        {
            if (item is null)
                continue;

            batch.Add(item);

            if (batch.Count >= batchSize)
            {
                yield return batch;
                batch = new List<ImportEnvelope>(batchSize);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }
}
