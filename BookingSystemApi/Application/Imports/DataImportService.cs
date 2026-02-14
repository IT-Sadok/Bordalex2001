using Application.Imports.Interfaces;
using System.Runtime.CompilerServices;

namespace Application.Imports;

public class DataImportService : IDataImportService
{
    
    public async Task ProcessImportAsync(Guid importJobId, CancellationToken ct = default)
    {
         
    }
}
