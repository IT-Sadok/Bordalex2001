using Application.Features.Imports.Interfaces;

namespace Application.Imports;

public class DataImportService(IJsonBatchReader reader, IImportJobRepository jobRepo) : IDataImportService
{
    private const int BatchSize = 1000;
    private const int BufferSize = 1024 * 128;

    public async Task ProcessImportAsync(Guid jobId, string filePath, CancellationToken ct = default)
    {
         await jobRepo.MarkInProgressAsync(jobId, ct);

         await using var stream = new FileStream(
             filePath, 
             FileMode.Open, 
             FileAccess.Read, 
             FileShare.Read, 
             BufferSize, 
             FileOptions.SequentialScan);
         
        await foreach (var batch in reader.ReadBatchesAsync(stream, BatchSize, ct))
        {
            await jobRepo.IncrementProcessedAsync(jobId, batch.Count, ct);
            await Task.Delay(100, ct);
        }

        await jobRepo.MarkCompletedAsync(jobId, ct);
    }
}
