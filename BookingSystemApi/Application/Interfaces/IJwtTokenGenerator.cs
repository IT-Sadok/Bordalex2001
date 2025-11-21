namespace Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string name, IEnumerable<string> roles);
}
