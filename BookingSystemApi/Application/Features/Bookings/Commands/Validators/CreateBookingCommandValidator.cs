using FluentValidation;

namespace Application.Features.Bookings.Commands.Validators;

public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.ApartmentId)
            .NotEmpty().WithMessage("ApartmentId is required.")
            .Must(id => id != Guid.Empty).WithMessage("ApartmentId cannot be an empty GUID.")
            .Must(id => !id.ToString().Contains(' ')).WithMessage("ApartmentId cannot contain spaces.");
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required.")
            .LessThan(x => x.EndDate).WithMessage("StartDate must be earlier than EndDate.")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).WithMessage("StartDate cannot be in the past.");
        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required.")
            .GreaterThan(x => x.StartDate).WithMessage("EndDate must be later than StartDate.");
    }
}
