namespace Application.Interfaces;

public interface ICompanyDataExportService
{
    Task ExportAsync(Stream output, CancellationToken ct);
}
