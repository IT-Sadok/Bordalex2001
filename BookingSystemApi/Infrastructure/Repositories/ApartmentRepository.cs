using Application.Features.Apartments.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Enums;
using System.Data;

namespace Infrastructure.Repositories;

public class ApartmentRepository(IDbConnection dbConnection) : IApartmentRepository
{
    public async Task<Apartment?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await dbConnection.QueryFirstOrDefaultAsync<Apartment>(
            "SELECT * FROM Apartments WHERE Id = @Id",
            new { Id = id },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<Apartment>> GetAvailableAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        return await dbConnection.QueryAsync<Apartment>(
            "SELECT * FROM Apartments WHERE IsAvailable = 1 AND Id NOT IN (SELECT ApartmentId FROM Bookings WHERE StartDate < @EndDate AND EndDate > @StartDate)",
            new { StartDate = startDate, EndDate = endDate },
            commandType: CommandType.StoredProcedure);
    }

    public async Task<(IEnumerable<Apartment>, int)> GetPagedAsync(
        int pageNumber = 1, 
        int pageSize = 20, 
        SortDirection sortDirection = SortDirection.Ascending,
        ApartmentSortBy sortBy = ApartmentSortBy.CreatedAt,
        CancellationToken ct = default)
    {
        if (pageNumber < 1) pageNumber = 1;
        if (pageSize < 1) pageSize = 20;

        var offset = (pageNumber - 1) * pageSize;
        var sortOrder = sortDirection == SortDirection.Ascending ? "ASC" : "DESC";
        var sortColumn = sortBy switch
        {
            ApartmentSortBy.PricePerNight => "PricePerNight",
            ApartmentSortBy.Title => "Title",
            ApartmentSortBy.Address => "Address",
            _ => "CreatedAt"
        };
        var query = $@"SELECT * FROM Apartments 
                       ORDER BY {sortColumn} {sortOrder} 
                       OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
                       SELECT COUNT(*) FROM Apartments;";

        using var multiple = await dbConnection.QueryMultipleAsync(query, new
        {
            Offset = offset,
            PageSize = pageSize,
        });

        var items = await multiple.ReadAsync<Apartment>();
        var totalCount = await multiple.ReadFirstAsync<int>();

        return (items, totalCount);
    }
}
