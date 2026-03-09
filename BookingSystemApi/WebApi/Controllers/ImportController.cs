using Application.Features.Imports.Interfaces;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/import")]
[ApiController]
public class ImportController(IImportStorage storage, IImportJobRepository jobRepo, ILogger<ImportController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> ImportData(IFormFile file, CancellationToken ct)
    {
        if (file.Length == 0)
        {
            return BadRequest("File is empty.");
        }

        var jobId = Guid.NewGuid();

        var filePath = await storage.SaveFileAsync(jobId, file, ct);

        await jobRepo.CreateAsync(new ImportJob
        {
            Id = jobId,
            FilePath = filePath,
            Status = ImportStatus.Pending,
            CreatedAt = DateTime.UtcNow
        }, ct);

        logger.LogInformation("Import job {JobId} created", jobId);

        return Accepted(new { JobId = jobId });
    }
}