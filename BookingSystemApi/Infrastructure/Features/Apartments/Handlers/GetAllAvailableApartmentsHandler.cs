using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.Queries;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Common;

namespace Infrastructure.Features.Apartments.Handlers;

public class GetAllAvailableApartmentsHandler(IApartmentRepository apartmentRepository) : IRequestHandler<GetAllAvailableApartmentsQuery, PagedResult<Apartment>>
{
    public async Task<PagedResult<Apartment>> HandleAsync(GetAllAvailableApartmentsQuery request, CancellationToken ct = default)
    {
        var (apartments, totalCount) = await apartmentRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            request.SortDirection,
            ct
        );

        return new PagedResult<Apartment>(
            apartments,
            totalCount,
            request.PageNumber,
            request.PageSize
        );
    }
}
