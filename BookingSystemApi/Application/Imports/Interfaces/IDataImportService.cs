namespace Application.Imports.Interfaces;

public interface IDataImportService
{
    Task ProcessImportAsync(Guid importJobId, CancellationToken ct = default);
}
