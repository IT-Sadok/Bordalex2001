using Application.Common.Mediator.Interfaces;
using Application.Features.Apartments.DTOs;
using Domain.Entities.Common;
using Domain.Entities.Enums;

namespace Application.Features.Apartments.Queries;

public class GetAllAvailableApartmentsQuery : IRequest<PagedResult<ApartmentListItemDto>>
{
    public SortDirection SortDirection { get; set; }
    public ApartmentSortBy SortBy { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
