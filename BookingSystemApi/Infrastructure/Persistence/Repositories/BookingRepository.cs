using Application.Features.Bookings.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Entities.Common;
using System.Data;

namespace Infrastructure.Persistence.Repositories;

public class BookingRepository(IDbConnection dbConnection) : IBookingRepository
{
    public async Task<PagedResult<Booking>> GetActiveBookingsAsync(Guid clientId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 20;

        var today = DateTime.UtcNow.Date;
        var offset = (pageNumber - 1) * pageSize;

        const string itemsQuery = @"
            SELECT * FROM ""Bookings"" 
            WHERE ""ClientId"" = @ClientId AND ""EndDate"" >= @Today
            ORDER BY ""StartDate"" ASC
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

        const string countQuery = @"
            SELECT COUNT(*) FROM ""Bookings"" 
            WHERE ""ClientId"" = @ClientId AND ""EndDate"" >= @Today;";

        using var multiple = await dbConnection.QueryMultipleAsync(
            $"{itemsQuery} {countQuery}",
            new
            {
                ClientId = clientId,
                Today = today,
                Offset = offset,
                PageSize = pageSize
            });

        var items = await multiple.ReadAsync<Booking>();
        var totalCount = await multiple.ReadFirstAsync<int>();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        return new PagedResult<Booking>(
            items,
            totalCount,
            pageNumber,
            pageSize
        );
    }

    public async Task<bool> HasOverlappingBookingAsync(Guid apartmentId, DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        var count = await dbConnection.ExecuteScalarAsync<int>(
            "SELECT COUNT(*) FROM \"Bookings\" WHERE \"ApartmentId\" = @ApartmentId AND \"StartDate\" < @EndDate AND \"EndDate\" > @StartDate",
            new
            {
                ApartmentId = apartmentId,
                StartDate = startDate,
                EndDate = endDate
            }
        );

        var hasAny = count > 0;
        return hasAny;
    }

    public async Task CreateAsync(Booking booking, CancellationToken ct = default)
    {
        var parameters = new
        {
            booking.Id,
            booking.ApartmentId,
            booking.ClientId,
            StartDate = booking.StartDate.ToDateTime(TimeOnly.MinValue),
            EndDate = booking.EndDate.ToDateTime(TimeOnly.MinValue),
            booking.TotalPrice
        };

        await dbConnection.ExecuteAsync(
            new CommandDefinition(
                "INSERT INTO \"Bookings\" (\"Id\", \"ApartmentId\", \"ClientId\", \"StartDate\", \"EndDate\", \"TotalPrice\") VALUES (@Id, @ApartmentId, @ClientId, @StartDate, @EndDate, @TotalPrice)",
                parameters,
                cancellationToken: ct
            )
        );
    }
}
