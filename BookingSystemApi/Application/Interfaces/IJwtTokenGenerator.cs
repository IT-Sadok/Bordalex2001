namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(Guid userId, string email, IEnumerable<string> roles);
}
