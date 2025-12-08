using Application.Common.Mediator.Interfaces;
using Domain.Entities;
using Domain.Entities.Common;

namespace Application.Features.Apartments.Queries;

public class GetAllAvailableApartmentsQuery : IRequest<PagedResult<Apartment>>
{
    public SortDirection SortDirection { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
