using Application.Imports.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Imports;

public class FileSystemImportStorage : IImportStorage
{
    private readonly string rootDirectory = "ImportStorage";

    public async Task<string> SaveFileAsync(IFormFile file, CancellationToken ct = default)
    {
        var fileName = $"{Guid.NewGuid()}.json";
        var filePath = Path.Combine(rootDirectory, fileName);

        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream, ct);

        return filePath;
    }
}
