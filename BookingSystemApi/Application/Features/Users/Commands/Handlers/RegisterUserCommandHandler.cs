using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Interfaces;

namespace Application.Features.Users.Commands.Handlers;

public class RegisterUserCommandHandler(IUserRegistrationService registrationService) : IRequestHandler<RegisterUserCommand, Guid>
{
    public Task<Guid> HandleAsync(RegisterUserCommand request, CancellationToken ct = default)
    {
        return registrationService.RegisterAsync(
            request.Email,
            request.Password,
            request.DisplayName,
            request.DateOfBirth,
            ct);
    }
}