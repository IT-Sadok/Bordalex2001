using MediatR;

namespace Application.Features.Bookings.Commands;

public class CreateBookingCommand : IRequest<Guid>
{
    public Guid ApartmentId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}
