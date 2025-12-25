using Application.Common.Mediator.Interfaces;
using Application.Features.Bookings.Commands;
using Application.Features.Bookings.Queries;
using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController(IRequestExecutor request) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingCommand command)
        {
            var bookingId = await request.ExecuteAsync<CreateBookingCommand, Guid>(command);
            return Ok(new { BookingId = bookingId });
        }

        [HttpGet("active")]
        public async Task<IActionResult> GetAllActiveBookingsAsync([FromQuery] int page, [FromQuery] int size, [FromQuery] string? sort = "asc")
        {
            var sortDirection = sort?.ToLower() == "desc" ? SortDirection.Descending : SortDirection.Ascending;
            
            var query = new GetAllActiveBookingsQuery
            {
                PageNumber = page,
                PageSize = size,
                SortDirection = sortDirection
            };

            var result = await request.ExecuteAsync<GetAllActiveBookingsQuery, PagedResult<Booking>>(query);

            return Ok(result);
        }
    }
}
