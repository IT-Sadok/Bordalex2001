using Application.Imports.Interfaces;
using Application.Interfaces;
using System.Runtime.CompilerServices;

namespace Application.Imports;

public class DataImportService : IDataImportService
{
    private readonly IImportStorage storage;
    private readonly IJsonBatchReader reader;
    private readonly IHostRepository hostRepo;
    private readonly IApartmentRepository apartmentRepo;
    private readonly IUserProvis userProvisioning;
    private readonly IImportJobRepository jobRepo;
    private readonly IUnitOfWork uow;

    public async Task ProcessImportAsync(Guid importJobId, CancellationToken ct = default)
    {
         
    }
}
