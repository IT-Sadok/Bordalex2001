using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.Queries;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/apartments")]
[ApiController]
public class ApartmentController(IRequestExecutor request) : ControllerBase
{
    [HttpGet("available")]
    public async Task<IActionResult> GetAllAvailableApartmentsAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string? sort = "asc", [FromQuery] string? sortBy = "date")
    {
        var sortDirection = sort?.ToLower() == "desc" ? SortDirection.Descending : SortDirection.Ascending;

        var sortByEnum = sortBy?.ToLower() switch
        {
            "price" => ApartmentSortBy.PricePerNight,
            "title" => ApartmentSortBy.Title,
            "address" => ApartmentSortBy.Address,
            _ => ApartmentSortBy.CreatedAt
        };

        var query = new GetAllAvailableApartmentsQuery
        {
            PageNumber = page,
            PageSize = size,
            SortDirection = sortDirection,
            SortBy = sortByEnum
        };

        var result = await request.ExecuteAsync<GetAllAvailableApartmentsQuery, PagedResult<Apartment>>(query);

        return Ok(result);
    }
}
