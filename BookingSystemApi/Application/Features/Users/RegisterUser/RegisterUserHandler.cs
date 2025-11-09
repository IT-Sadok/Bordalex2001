using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.RegisterUser;

public class RegisterUserHandler(IUserRepository repository, IPasswordHasher<User> hasher) : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken ct)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = hasher.HashPassword(null, request.Password),
            Name = request.Name,
            Role = Enum.Parse<UserRole>(request.Role, true)
        };

        await repository.AddUserAsync(user);
        return user.Id;
    }
}
