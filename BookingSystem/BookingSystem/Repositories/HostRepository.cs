using BookingSystem.Models;

namespace BookingSystem.Repositories;

public class HostRepository
{
    private readonly List<Host> _hosts = [];

    public void CreateHost(Host host) => _hosts.Add(host);

    public IEnumerable<Host> DisplayHosts() => _hosts;

    public Host? GetHostById(int id) => _hosts.Find(h => h.Id == id);

    public void EditHost(Host host)
    {
        var existingHost = GetHostById(host.Id);
        if (existingHost != null)
        {
            existingHost.Name = host.Name;
        }
    }

    public void DeleteHost(int id)
    {
        var host = GetHostById(id);
        if (host != null)
        {
            _hosts.Remove(host);
        }
    }
}
