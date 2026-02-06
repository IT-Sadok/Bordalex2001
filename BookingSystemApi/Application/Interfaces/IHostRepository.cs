using Application.Exports.DTOs;

namespace Application.Interfaces;

public interface IHostRepository
{
    IAsyncEnumerable<HostExportDto> StreamHostsAsync(CancellationToken ct);
}
