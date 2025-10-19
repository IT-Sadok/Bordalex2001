using BookingSystem.Models;
using System.Text.Json;

namespace BookingSystem.Repositories;

public class HostRepository : IHostRepository
{
    private readonly string _filePath = "hosts.json";
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

    public void LoadHosts()
    {
        try 
        {
            if (!File.Exists(_filePath))
            {
                return;
            }
            
            var json = File.ReadAllText(_filePath);
            var hosts = JsonSerializer.Deserialize<List<Host>>(json);
            if (hosts != null)
            {
                _hosts.Clear();
                _hosts.AddRange(hosts);
            }
        }
        catch
        {
            throw;
        }
    }

    public void SaveHosts()
    {
        try 
        {
            JsonSerializerOptions options = new() { WriteIndented = true };
            var json = JsonSerializer.Serialize(_hosts, options);
            File.WriteAllText(_filePath, json);
        }
        catch
        {
            throw;
        }
    }
}
