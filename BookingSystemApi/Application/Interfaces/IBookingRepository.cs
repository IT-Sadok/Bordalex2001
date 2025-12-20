using Domain.Entities;

namespace Application.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetActiveBookingsAsync(Guid clientId, CancellationToken ct = default);
    Task<bool> HasOverlappingBookingAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task CreateAsync(Booking booking, CancellationToken ct = default);
}
