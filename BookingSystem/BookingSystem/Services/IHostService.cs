using BookingSystem.Models;

namespace BookingSystem.Services;

public interface IHostService
{
    IEnumerable<Host> DisplayHosts();
    Host? GetHostById(int id);
    void CreateHost(string name);
    void EditHost(int id);
    void DeleteHost(int id);
    void LoadHosts();
    void SaveHosts();
}
