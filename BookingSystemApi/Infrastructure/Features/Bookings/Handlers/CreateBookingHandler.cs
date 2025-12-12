using Application.Common.Mediator.Interfaces;
using Application.Features.Bookings.Commands;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.UserContext;

namespace Infrastructure.Features.Bookings.Handlers;

public class CreateBookingHandler(IBookingRepository bookingRepository, IApartmentRepository apartmentRepository, IUserContext userContext) : IRequestHandler<CreateBookingCommand, Guid>
{
    public async Task<Guid> HandleAsync(CreateBookingCommand request, CancellationToken ct = default) 
    { 
        var userContextData = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException("User is not authenticated.");

        var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId, ct);
        if (!apartment.IsAvailable)
        {
            throw new InvalidOperationException("Apartment is not available for booking.");
        }

        var totalNights = request.EndDate.DayNumber - request.StartDate.DayNumber;

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ClientId = Guid.Parse(userContextData.Id),
            ApartmentId = request.ApartmentId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalPrice = totalNights * apartment.PricePerNight
        };

        await bookingRepository.CreateAsync(booking, ct);
        return booking.Id;
    }
}
