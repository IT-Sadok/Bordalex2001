using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

/*[Route("api/data-migration")]
[ApiController]
public class DataMigrationController(ICompanyDataMigrationService service) : ControllerBase
{
    [HttpPost("export")]
    public async Task<IActionResult> Export(CancellationToken ct)
    {
        Response.ContentType = "application/json";
        Response.Headers.ContentDisposition = "attachment; filename=company-export.json";

        await service.ExportAsync(Response.Body, ct);
        return new EmptyResult();
    }
}*/
