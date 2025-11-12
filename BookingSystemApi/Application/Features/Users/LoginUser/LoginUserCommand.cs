using MediatR;

namespace Application.Features.Users.LoginUser;

public record LoginUserCommand(
    string Email, 
    string Password) : IRequest<string>;