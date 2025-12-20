using Application.Interfaces;
using Dapper;
using Domain.Entities;
using System.Data;

namespace Infrastructure.Repositories;

public class BookingRepository(IDbConnection dbConnection) : IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetActiveBookingsAsync(Guid clientId, CancellationToken ct = default)
    {
        return await dbConnection.QueryAsync<Booking>(
            "SELECT * FROM Bookings WHERE ClientId = @ClientId AND EndDate >= @Today",
            new { ClientId = clientId, Today = DateTime.UtcNow.Date },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<bool> HasOverlappingBookingAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        return await dbConnection.ExecuteScalarAsync<bool>(
            "SELECT COUNT(1) FROM Bookings WHERE ApartmentId = @ApartmentId AND StartDate < @EndDate AND EndDate > @StartDate",
            new { ApartmentId = apartmentId, StartDate = startDate, EndDate = endDate },
            commandType: CommandType.StoredProcedure);
    }

    public async Task CreateAsync(Booking booking, CancellationToken ct = default)
    {
        await dbConnection.ExecuteAsync(
            "INSERT INTO Bookings (Id, ApartmentId, ClientId, StartDate, EndDate, TotalPrice) VALUES (@Id, @ApartmentId, @ClientId, @StartDate, @EndDate, @TotalPrice)",
            new
            {
                booking.Id,
                booking.ApartmentId,
                booking.ClientId,
                booking.StartDate,
                booking.EndDate,
                booking.TotalPrice
            },
            commandType: CommandType.StoredProcedure);
    }
}
