using Application.Features.Apartments.DTOs;
using Domain.Entities;
using Domain.Entities.Common;
using Domain.Entities.Enums;

namespace Application.Features.Apartments.Interfaces;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IEnumerable<Apartment>> GetAvailableAsync(DateTime startDate, DateTime endDate, CancellationToken ct = default);
    Task<(IEnumerable<ApartmentListItemDto>, int)> GetPagedAsync(int pageNumber, int pageSize, SortDirection sortDirection, ApartmentSortBy sortBy, string? destination = null, CancellationToken ct = default);
}
