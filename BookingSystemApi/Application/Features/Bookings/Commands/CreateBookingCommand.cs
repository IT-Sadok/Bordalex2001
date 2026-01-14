using Application.Common.Mediator.Interfaces;

namespace Application.Features.Bookings.Commands;

public record CreateBookingCommand(
    Guid ApartmentId,
    DateOnly StartDate,
    DateOnly EndDate) : IRequest<Guid>;
