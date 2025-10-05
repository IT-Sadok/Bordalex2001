using BookingSystem.Models;
using BookingSystem.Repositories;

namespace BookingSystem.Services;

public class HostService
{
    private readonly HostRepository _repository;

    public HostService(HostRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Host> DisplayHosts() => _repository.DisplayHosts();

    public Host? GetHostById(int id) => _repository.GetHostById(id);

    public void CreateHost(string name)
    {
        var host = new Host
        {
            Id = GenerateId(),
            Name = name
        };
        _repository.CreateHost(host);
    }

    public void EditHost(int id, string newName)
    {
        var host = _repository.GetHostById(id);
        if (host != null)
        {
            host.Name = newName;
            _repository.EditHost(host);
        }
    }

    public void DeleteHost(int id) => _repository.DeleteHost(id);

    private int GenerateId()
    {
        var hosts = _repository.DisplayHosts();
        return hosts.Any() ? hosts.Max(h => h.Id) + 1 : 1;
    }
}
