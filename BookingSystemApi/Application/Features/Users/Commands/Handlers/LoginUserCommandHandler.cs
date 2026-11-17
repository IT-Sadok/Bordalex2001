using Application.Common.Mediator.Interfaces;
using Application.Features.Users.Interfaces;

namespace Application.Features.Users.Commands.Handlers;

public class LoginUserCommandHandler(IUserAuthenticationService authenticationService) : IRequestHandler<LoginUserCommand, string>
{
    public Task<string> HandleAsync(LoginUserCommand request, CancellationToken ct = default)
    {
        return authenticationService.LoginAsync(
            request.Email,
            request.Password,
            ct);
    }
}