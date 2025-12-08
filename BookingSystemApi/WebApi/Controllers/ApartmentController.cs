using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.Queries;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/apartments")]
[ApiController]
public class ApartmentController(IRequestExecutor request) : ControllerBase
{
    [HttpGet("available")]
    public async Task<IActionResult> GetAllAvailableApartmentsAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string sort) 
    {
        var sortDirection = sort?.ToLower() == "desc" ? SortDirection.Descending : SortDirection.Ascending;
        var query = new GetAllAvailableApartmentsQuery
        {
            PageNumber = page,
            PageSize = size,
            SortDirection = sortDirection
        };
        var result = await request.ExecuteAsync<GetAllAvailableApartmentsQuery, PagedResult<Apartment>>(query);
        return Ok(result);
    }
}
