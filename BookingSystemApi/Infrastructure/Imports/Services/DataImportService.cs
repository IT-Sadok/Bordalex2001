using Application.Imports.Interfaces;
using Application.Interfaces;

namespace Application.Imports;

public class DataImportService(IJsonBatchReader reader, IImportJobRepository jobRepo) : IDataImportService
{
    public async Task ProcessImportAsync(Guid jobId, string filePath, CancellationToken ct = default)
    {
         await jobRepo.MarkInProgressAsync(jobId, ct);

         await using var stream = new FileStream(
             filePath, 
             FileMode.Open, 
             FileAccess.Read, 
             FileShare.Read, 
             bufferSize: 1024 * 128, 
             FileOptions.SequentialScan);
         
        await foreach (var batch in reader.ReadBatchesAsync(stream, batchSize: 500, ct))
        {
            await jobRepo.IncrementProcessedAsync(jobId, batch.Count, ct);
            
            await Task.Delay(100, ct);
        }

        await jobRepo.MarkCompletedAsync(jobId, ct);
    }
}
