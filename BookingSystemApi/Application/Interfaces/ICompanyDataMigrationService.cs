namespace Application.Interfaces;

public interface ICompanyDataMigrationService
{
    Task ExportAsync(Stream output, CancellationToken ct);
}
