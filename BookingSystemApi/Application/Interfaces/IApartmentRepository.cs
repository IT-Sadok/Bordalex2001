using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Enums;

namespace Application.Interfaces;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Apartment>> GetAvailableAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task<(IEnumerable<Apartment>, int)> GetPagedAsync(int pageNumber, int pageSize, SortDirection sortDirection, ApartmentSortBy sortBy, CancellationToken ct = default);
}
