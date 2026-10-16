using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.DTOs;
using Application.Features.Apartments.Interfaces;
using Application.Features.Apartments.Queries;
using Domain.Entities.Common;

namespace Infrastructure.Features.Apartments.Handlers;

public class GetAllAvailableApartmentsHandler(IApartmentRepository apartmentRepository) : IRequestHandler<GetAllAvailableApartmentsQuery, PagedResult<ApartmentListItemDto>>
{
    public async Task<PagedResult<ApartmentListItemDto>> HandleAsync(GetAllAvailableApartmentsQuery request, CancellationToken ct = default)
    {
        var (apartments, totalCount) = await apartmentRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SortDirection,
            request.SortBy,
            ct
        );

        return new PagedResult<ApartmentListItemDto>(
            apartments,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }
}
