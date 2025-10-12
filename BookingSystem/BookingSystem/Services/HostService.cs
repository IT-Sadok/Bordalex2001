using BookingSystem.Logging;
using BookingSystem.Models;
using BookingSystem.Repositories;

namespace BookingSystem.Services;

public class HostService(HostRepository repository, ILogger logger) : IHostService
{
    private readonly HostRepository _repository = repository;
    private readonly ILogger _logger = logger;

    public IEnumerable<Host> DisplayHosts() => _repository.DisplayHosts();

    public Host? GetHostById(int id) => _repository.GetHostById(id);

    public void CreateHost(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Host name shouldn't be empty. Please try again.");
            return;
        }

        var host = new Host
        {
            Id = GenerateId(),
            Name = name
        };
        _repository.CreateHost(host);
        _logger.LogInfo("Host created successfully.");
    }

    public void EditHost(int id)
    {
        var host = _repository.GetHostById(id);
        if (host == null)
        {
            _logger.LogError("Host not found.");
            return;
        }

        Console.Write("Enter a new host name: ");
        var newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
        {
            _logger.LogWarning("Host name shouldn't be empty. Please try again.");
            return;
        }

        host.Name = newName;
        _repository.EditHost(host);
        _logger.LogInfo("Host edited successfully.");
    }

    public void DeleteHost(int id)
    {
        var host = _repository.GetHostById(id);
        if (host == null)
        {
            _logger.LogError("Host not found.");
            return;
        }

        _repository.DeleteHost(id);
        _logger.LogInfo($"Host deleted successfully.");
    }

    private int GenerateId()
    {
        var hosts = _repository.DisplayHosts();
        return hosts.Any() ? hosts.Max(h => h.Id) + 1 : 1;
    }
}
