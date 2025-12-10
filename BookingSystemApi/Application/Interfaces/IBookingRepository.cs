using Domain.Entities;

namespace Application.Interfaces;

public interface IBookingRepository
{
    Task<IEnumerable<Booking>> GetClientBookingsAsync(Guid clientId, CancellationToken ct = default);
    Task<bool> IsAvailableAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task CreateAsync(Booking booking, CancellationToken ct = default);
}
