using Application.Common.Mediator.Interfaces;
using Application.Features.Bookings.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/bookings")]
    [ApiController]
    public class BookingController(IRequestExecutor request) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingCommand command)
        {
            var bookingId = await request.ExecuteAsync<CreateBookingCommand, Guid>(command);
            return Ok(new { BookingId = bookingId });
        }
    }
}
