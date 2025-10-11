using BookingSystem.Logging;
using BookingSystem.Repositories;
using BookingSystem.Services;

namespace BookingSystem;

public class Program
{
    private static readonly ILogger logger = new ConsoleLogger();
    private static readonly HostRepository hostRepository = new(logger);
    private static readonly HostService hostService = new(hostRepository, logger);

    public static void Main(string[] args)
    {
        bool showMenu = true;
        while (showMenu)
        {
            Console.WriteLine("\nBooking System Menu:");
            Console.WriteLine("1. Create Hosts");
            Console.WriteLine("2. Display Hosts");
            Console.WriteLine("3. Edit Host");
            Console.WriteLine("4. Delete Host");
            Console.WriteLine("5. Exit");
            Console.Write("\nChoose an option (1-5): ");
            var choice = Console.ReadLine();
            Console.WriteLine();
            switch (choice)
            {
                case "1":
                    CreateHost();
                    break;
                case "2":
                    DisplayHosts();
                    break;
                case "3":
                    EditHost();
                    break;
                case "4":
                    DeleteHost();
                    break;
                case "5":
                    showMenu = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose a valid option.");
                    break;
            }
        }
        /*var selectedHost = DisplayHostById(hosts);
        if (selectedHost != null)
        {
            DisplayApartments(selectedHost);
        }*/
    }

    private static void CreateHost()
    {
        Console.Write("Enter a host name: ");
        var name = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(name))
        {
            hostService.CreateHost(name);
            Console.WriteLine("Host created successfully!");
        }
        else
        {
            Console.WriteLine("Host name shouldn't be empty. Please try again.");
        }
    }

    private static void DisplayHosts()
    {
        Console.WriteLine("List of hosts:\n");
        foreach (var host in hostService.DisplayHosts())
        {
            Console.WriteLine($"Host ID: {host.Id}, Name: {host.Name}");
        }
    }

    /*private static Host? DisplayHostById(List<Host> hosts)
    {
        Console.Write("\nEnter a host ID to display its apartments: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var host = hosts.Find(h => h.Id == id);
            if (host == null)
            {
                Console.WriteLine("\nHost not found");
            }
            return host;
        }
        Console.WriteLine("\nInvalid input. Please enter a valid host ID.");
        return null;
    }*/

    /*private static void DisplayApartments(Host host)
    {
        Console.WriteLine($"\nApartments for {host.Name}\n");
        foreach (var apartment in host.Apartments)
        {
            Console.WriteLine($"Apartment ID: {apartment.Id}, Name: {apartment.Name}");
        }
    }*/

    private static void EditHost()
    {
        Console.Write("Enter the host ID you want to edit: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            //var hostToEdit = hosts.Find(h => h.Id == id);
            //if (hostToEdit != null)
            //{
                Console.Write("Enter the new name for the host: ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    hostService.EditHost(id, newName);
                    Console.WriteLine("Host edited successfully!");
                }
                else
                {
                    Console.WriteLine("Host name shouldn't be empty. Please try again.");
                }
            //}
            //else
            //{
                //Console.WriteLine("Host not found");
            //}
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid host ID.");
        }
    }

    private static void DeleteHost()
    {
        Console.Write("Enter the host ID you want to delete: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            //var hostToDelete = hosts.Find(h => h.Id == id);
            //if (hostToDelete != null)
            //{
                hostService.DeleteHost(id);
                Console.WriteLine("Host deleted successfully!");
            //}
            //else
            //{
                //Console.WriteLine("Host not found");
            //}
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid host ID.");
        }
    }
}