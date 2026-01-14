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
        var user = userContext.GetCurrentUser() ?? throw new UnauthorizedAccessException("User is not authenticated.");

        if (request.EndDate <= request.StartDate)
        {
            throw new InvalidOperationException("End date must be after start one.");
        }

        var apartment = await apartmentRepository.GetByIdAsync(request.ApartmentId, ct) ?? throw new InvalidOperationException("Apartment not found.");

        var hasBookingConflict = await bookingRepository.HasOverlappingBookingAsync(request.ApartmentId, request.StartDate.ToDateTime(TimeOnly.MinValue), request.EndDate.ToDateTime(TimeOnly.MinValue), ct);

        if (hasBookingConflict)
        {
            throw new InvalidOperationException("The apartment is already booked for the selected dates.");
        }

        var totalNights = request.EndDate.DayNumber - request.StartDate.DayNumber;

        var booking = new Booking
        {
            Id = Guid.NewGuid(),
            ClientId = Guid.Parse(user.Id),
            ApartmentId = request.ApartmentId,
            StartDate = request.StartDate,
            EndDate = request.EndDate,
            TotalPrice = totalNights * apartment.PricePerNight
        };

        await bookingRepository.CreateAsync(booking, ct);
        return booking.Id;
    }
}
