using MediatR;

namespace Application.Features.Users.RegisterUser;
public record RegisterUserCommand(
    string Email, 
    string Password, 
    string Name, 
    string Role) 
    : IRequest<Guid>;