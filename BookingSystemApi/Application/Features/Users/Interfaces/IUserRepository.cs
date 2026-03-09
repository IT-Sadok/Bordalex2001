using Application.Features.Imports.DTOs;

namespace Application.Features.Users.Interfaces;

public interface IUserRepository
{
    IAsyncEnumerable<HostImportDto> StreamHostsAsync(CancellationToken ct = default);
}
