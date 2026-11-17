namespace Application.Features.Users.Interfaces;

public interface IUserAuthenticationService
{
    Task<string> LoginAsync(
        string email,
        string password,
        CancellationToken ct = default);
}
