using Application.Imports.Models.DTOs;

namespace Application.Interfaces;

public interface IHostRepository
{
    IAsyncEnumerable<HostImportDto> StreamHostsAsync(CancellationToken ct = default);
}
