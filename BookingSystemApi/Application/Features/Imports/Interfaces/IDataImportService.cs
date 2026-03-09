namespace Application.Features.Imports.Interfaces;

public interface IDataImportService
{
    Task ProcessImportAsync(Guid jobId, string filePath, CancellationToken ct = default);
}
