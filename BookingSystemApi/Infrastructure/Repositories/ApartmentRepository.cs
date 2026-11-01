using Application.Features.Apartments.DTOs;
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
        return await dbConnection.QuerySingleOrDefaultAsync<Apartment>(
            "SELECT * FROM \"Apartments\" WHERE \"Id\" = @Id",
            new { Id = id }
        );
    }

    public async Task<IEnumerable<Apartment>> GetAvailableAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default)
    {
        return await dbConnection.QueryAsync<Apartment>(
            "SELECT * FROM \"Apartments\" WHERE \"IsAvailable\" AND \"Id\" NOT IN (SELECT \"ApartmentId\" FROM \"Bookings\" WHERE \"StartDate\" < @EndDate AND \"EndDate\" > @StartDate)",
            new { StartDate = startDate, EndDate = endDate }
        );
    }

    public async Task<(IEnumerable<ApartmentListItemDto>, int)> GetPagedAsync(
        int pageNumber = 1,
        int pageSize = 20,
        SortDirection sortDirection = SortDirection.Ascending,
        ApartmentSortBy sortBy = ApartmentSortBy.CreatedAt,
        string? destination = null,
        DateOnly? checkIn = null,
        DateOnly? checkOut = null,
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

        destination = string.IsNullOrWhiteSpace(destination) ? null : destination.Trim();

        var query = $@"
            SELECT
                a.""Id"",
                a.""Title"",
                a.""Address"",
                a.""PricePerNight"",
                ai.""ImageUrl"" AS ""PrimaryImageUrl""
            FROM ""Apartments"" AS a
            LEFT JOIN ""ApartmentImages"" AS ai
                ON ai.""ApartmentId"" = a.""Id""
                AND ai.""IsPrimary"" = TRUE
            WHERE a.""IsAvailable"" = TRUE
              AND a.""DeletedAt"" IS NULL
              AND (
                  @Destination IS NULL
                  OR a.""Address"" ILIKE '%' || @Destination || '%'
              )
              AND (
                  @CheckIn IS NULL
                  OR @CheckOut IS NULL
                  OR NOT EXISTS (
                      SELECT 1
                      FROM ""Bookings"" AS b
                      WHERE b.""ApartmentId"" = a.""Id""
                        AND b.""StartDate"" < @CheckOut
                        AND b.""EndDate"" > @CheckIn
                  )
              )
            ORDER BY a.""{sortColumn}"" {sortOrder}, a.""Id"" ASC
            LIMIT @PageSize OFFSET @Offset;

            SELECT COUNT(*)
            FROM ""Apartments"" AS a
            WHERE a.""IsAvailable"" = TRUE
              AND a.""DeletedAt"" IS NULL
              AND (
                  @Destination IS NULL
                  OR a.""Address"" ILIKE '%' || @Destination || '%'
              )
              AND (
                  @CheckIn IS NULL
                  OR @CheckOut IS NULL
                  OR NOT EXISTS (
                      SELECT 1
                      FROM ""Bookings"" AS b
                      WHERE b.""ApartmentId"" = a.""Id""
                        AND b.""StartDate"" < @CheckOut
                        AND b.""EndDate"" > @CheckIn
                  )
              );";

        var parameters = new DynamicParameters();

        parameters.Add("Offset", offset);
        parameters.Add("PageSize", pageSize);
        parameters.Add("Destination", destination, DbType.String);
        parameters.Add("CheckIn", checkIn, DbType.Date);
        parameters.Add("CheckOut", checkOut, DbType.Date);

        var command = new CommandDefinition(
            query,
            parameters,
            cancellationToken: ct
        );

        using var multiple = await dbConnection.QueryMultipleAsync(command);

        var items = await multiple.ReadAsync<ApartmentListItemDto>();
        var totalCount = await multiple.ReadSingleOrDefaultAsync<int>();

        return (items, totalCount);
    }
}
