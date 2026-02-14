namespace Application.Imports.Interfaces;

public interface IJsonBatchReader
{
    Task<IEnumerable<T>> ReadBatchAsync<T>(string filePath, int batchSize, CancellationToken ct = default);
}
