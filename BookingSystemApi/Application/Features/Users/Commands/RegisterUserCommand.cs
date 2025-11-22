using MediatR;

namespace Application.Features.Users.Commands;

public record RegisterUserCommand(
    string Email, 
    string Password, 
    string? DisplayName,
    DateOnly? DateOfBirth) : IRequest<Guid>;