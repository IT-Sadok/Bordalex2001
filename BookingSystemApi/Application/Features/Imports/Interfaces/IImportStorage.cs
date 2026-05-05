using Microsoft.AspNetCore.Http;

namespace Application.Features.Imports.Interfaces;

public interface IImportStorage
{
    Task<string> SaveFileAsync(Guid jobId, IFormFile file, CancellationToken ct = default);
}
