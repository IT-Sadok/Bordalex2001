using Application.Interfaces;
using Dapper;
using Domain.Entities;
using Domain.Entities.Common;
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

    public async Task<IEnumerable<Apartment>> GetAvailableApartmentsAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        return await dbConnection.QueryAsync<Apartment>(
            "SELECT * FROM Apartments WHERE IsAvailable = 1 AND Id NOT IN (SELECT ApartmentId FROM Bookings WHERE StartDate < @EndDate AND EndDate > @StartDate)",
            new { StartDate = startDate, EndDate = endDate },
            commandType: CommandType.StoredProcedure);
    }

    //public async Task<(IEnumerable<Apartment>, int)> GetPagedAsync(int pageNumber = 1, int //pageSize = 20, SortDirection sortDirection = SortDirection.Ascending, CancellationToken /ct /= default)
    //{
    //    if (pageNumber < 1) pageNumber = 1;
    //    if (pageSize < 1) pageSize = 20;
    //}
}
