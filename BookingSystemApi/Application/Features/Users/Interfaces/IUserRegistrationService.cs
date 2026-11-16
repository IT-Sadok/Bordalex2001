namespace Application.Features.Users.Interfaces;

public interface IUserRegistrationService
{
    Task<Guid> RegisterAsync(
        string email,
        string password,
        string? displayName,
        DateOnly? dateOfBirth,
        CancellationToken ct = default);
}
