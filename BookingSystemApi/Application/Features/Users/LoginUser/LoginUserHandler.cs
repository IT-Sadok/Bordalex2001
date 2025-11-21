using Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Users.LoginUser;

public class LoginUserHandler(
    IUserRepository userRepository,
    IPasswordHasher<AppUser> passwordHasher,
    IJwtTokenGenerator jwtGenerator) : IRequestHandler<LoginUserCommand, string>
{
    public async Task<string> Handle(LoginUserCommand request, CancellationToken ct)
    { 
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user is null)
        {
            await Task.FromException(new Exception("User not found."));
        }

        var verifyResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (verifyResult == PasswordVerificationResult.Failed)
        {
            await Task.FromException(new Exception("Wrong password."));
        }

        var jwt = jwtGenerator.GenerateToken(user);
        return jwt;
    }
}