using Application.Common.Mediator.Interfaces;

namespace Application.Features.Users.Commands;

public record LoginUserCommand(
    string Email, 
    string Password) : IRequest<string>;