using Application.Interfaces;
using Domain.Entities;

namespace Infrastructure.Repositories;

public class BookingRepository : IBookingRepository
{
    public Task<IEnumerable<Booking>> GetActiveBookingsAsync(Guid clientId, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsAvailableAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Booking booking, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
