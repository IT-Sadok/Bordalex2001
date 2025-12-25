using Application.Common.Mediator.Interfaces;
using Domain.Entities;
using Domain.Entities.Common;

namespace Application.Features.Bookings.Queries;

public class GetAllActiveBookingsQuery : IRequest<PagedResult<Booking>>
{
    public SortDirection SortDirection { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
