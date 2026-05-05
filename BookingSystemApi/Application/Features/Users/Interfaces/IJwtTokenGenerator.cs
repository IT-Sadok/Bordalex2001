namespace Application.Features.Users.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(Guid userId, string email, IEnumerable<string> roles);
}
