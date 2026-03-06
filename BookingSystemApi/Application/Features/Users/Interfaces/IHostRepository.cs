using Application.Features.Imports.DTOs;

namespace Application.Features.Users.Interfaces;

public interface IHostRepository
{
    IAsyncEnumerable<HostImportDto> StreamHostsAsync(CancellationToken ct = default);
}
