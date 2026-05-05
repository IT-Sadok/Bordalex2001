using Domain.Entities;
using Domain.Entities.Common;

namespace Application.Features.Bookings.Interfaces;

public interface IBookingRepository
{
    Task<PagedResult<Booking>> GetActiveBookingsAsync(Guid clientId, int pageNumber, int pageSize, CancellationToken ct = default);
    Task<bool> HasOverlappingBookingAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task CreateAsync(Booking booking, CancellationToken ct = default);
}
