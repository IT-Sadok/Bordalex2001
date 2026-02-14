using Microsoft.AspNetCore.Http;

namespace Application.Imports.Interfaces;

public interface IImportStorage
{
    Task<string> SaveAsync(IFormFile file, CancellationToken ct);
}
