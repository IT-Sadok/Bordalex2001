using Application.Features.Imports.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Imports;

public class FileSystemImportStorage(IWebHostEnvironment env) : IImportStorage
{
    private readonly string rootDirectory = Path.Combine(env.ContentRootPath, "ImportFiles");

    public async Task<string> SaveFileAsync(Guid jobId, IFormFile file, CancellationToken ct = default)
    {
        Directory.CreateDirectory(rootDirectory);

        var filePath = Path.Combine(rootDirectory, $"{jobId}.json");

        await using var stream = new FileStream(filePath, FileMode.Create);

        await file.CopyToAsync(stream, ct);

        return filePath;
    }
}
