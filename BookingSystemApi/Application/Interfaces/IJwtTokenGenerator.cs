namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(Guid userId, string userName, IEnumerable<string> roles);
}
