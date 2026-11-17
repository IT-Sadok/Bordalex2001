using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.DTOs;
using Application.Features.Apartments.Interfaces;
using Domain.Entities.Common;

namespace Application.Features.Apartments.Queries.Handlers;

public class GetAllAvailableApartmentsQueryHandler(IApartmentRepository apartmentRepository) : IRequestHandler<GetAllAvailableApartmentsQuery, PagedResult<ApartmentListItemDto>>
{
    public async Task<PagedResult<ApartmentListItemDto>> HandleAsync(GetAllAvailableApartmentsQuery request, CancellationToken ct = default)
    {
        var (apartments, totalCount) = await apartmentRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SortDirection,
            request.SortBy,
            request.Destination,
            request.CheckIn,
            request.CheckOut,
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
