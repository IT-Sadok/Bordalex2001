using Application.Common;
using Application.Common.Mediator.Interfaces;
using Application.Features.Bookings.Interfaces;
using Domain.Entities;
using Domain.Entities.Common;

namespace Application.Features.Bookings.Queries.Handlers;

public class GetAllActiveBookingsQueryHandler(IBookingRepository bookingRepository, IUserContext userContext) : IRequestHandler<GetAllActiveBookingsQuery, PagedResult<Booking>>
{
    public async Task<PagedResult<Booking>> HandleAsync(GetAllActiveBookingsQuery request, CancellationToken ct = default)
    {
        var user = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException("User is not authenticated.");

        return await bookingRepository.GetActiveBookingsAsync(
            Guid.Parse(user.Id),
            request.PageNumber,
            request.PageSize,
            ct
        );
    }
}
