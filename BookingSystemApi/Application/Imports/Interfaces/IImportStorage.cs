using Microsoft.AspNetCore.Http;

namespace Application.Imports.Interfaces;

public interface IImportStorage
{
    Task<string> SaveFileAsync(IFormFile file, CancellationToken ct = default);
}
