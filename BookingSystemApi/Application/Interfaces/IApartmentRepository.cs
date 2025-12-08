using Domain.Entities;
using Domain.Entities.Common;

namespace Application.Interfaces;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Apartment>> GetAvailableAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task<(IEnumerable<Apartment>, int)> GetPagedAsync(int pageNumber, int pageSize, SortDirection sortDirection, CancellationToken ct = default);
}
