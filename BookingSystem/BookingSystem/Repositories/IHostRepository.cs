using BookingSystem.Models;

namespace BookingSystem.Repositories;

public interface IHostRepository
{
    void CreateHost(Host host);
    IEnumerable<Host> DisplayHosts();
    Host? GetHostById(int id);
    void EditHost(Host host);
    void DeleteHost(int id);
}
