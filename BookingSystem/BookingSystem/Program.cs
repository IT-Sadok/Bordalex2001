using BookingSystem.Logging;
using BookingSystem.Models;
using BookingSystem.Repositories;
using BookingSystem.Services;

namespace BookingSystem;

public class Program
{
    private static readonly ILogger logger = new ConsoleLogger();
    private static readonly HostRepository hostRepository = new();
    private static readonly HostService hostService = new(hostRepository, logger);

    public static void Main(string[] args)
    {
        LoadHosts();

        bool showMenu = true;
        while (showMenu)
        {
            Console.WriteLine("\nBooking System Menu:");
            Console.WriteLine("1. Create Hosts");
            Console.WriteLine("2. Display Hosts");
            Console.WriteLine("3. Edit Host");
            Console.WriteLine("4. Delete Host");
            Console.WriteLine("5. Exit");
            Console.WriteLine("6. Save Hosts");
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
                case "6":
                    SaveHosts();
                    break;
                default:
                    logger.LogError("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private static void CreateHost()
    {
        Console.Write("Enter a host name: ");
        var name = Console.ReadLine();
        hostService.CreateHost(name);
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
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            logger.LogError("Invalid input. Please enter a valid host ID.");
            return;
        }
        else
        {
            hostService.EditHost(id);
        }
    }

    private static void DeleteHost()
    {
        Console.Write("Enter the host ID you want to delete: ");
        if (!int.TryParse(Console.ReadLine(), out var id))
        {
            logger.LogError("Invalid input. Please enter a valid host ID.");
            return;
        }
        else
        {
            hostService.DeleteHost(id);
        }
    }

    private static void LoadHosts()
    {
        hostService.LoadHosts();
    }

    private static void SaveHosts()
    {
        hostService.SaveHosts();
    }

    private static void SimulateMultithreading()
    {
        Console.WriteLine("Simulating multithreading environment...");

        var apartment = new Apartment(100);
        int iterations = 100;

        Thread host1 = new(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                logger.LogInfo($"Host 1 increasing price, iteration {i+1}");
                apartment.IncreasePriceUnsafely(10);
            }
        });
        Thread host2 = new(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                logger.LogInfo($"Host 2 increasing price, iteration {i+1}");
                apartment.IncreasePriceUnsafely(10);
            }
        });

        host1.Start();
        host2.Start();
        host1.Join();
        host2.Join();

        int expectedPrice = 100 + (2 * iterations * 10);
        Console.WriteLine($"Expected Price: {expectedPrice}");
        Console.WriteLine($"Final Price (without syncronization): {apartment.Price}");
        Console.WriteLine("Update loss occurred due to Race Condition.");

        apartment = new Apartment(100);
        host1 = new(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                logger.LogInfo($"Host 1 increasing price, iteration {i+1}");
                apartment.IncreasePriceSafely(10);
            }
        });
        host2 = new(() =>
        {
            for (int i = 0; i < iterations; i++)
            {
                logger.LogInfo($"Host 2 increasing price, iteration {i+1}");
                apartment.IncreasePriceSafely(10);
            }
        });

        host1.Start();
        host2.Start();
        host1.Join();
        host2.Join();

        Console.WriteLine($"Expected Price: {expectedPrice}");
        Console.WriteLine($"Final Price (with syncronization): {apartment.Price}");
        Console.WriteLine($"Update loss prevented with proper locking mechanism.");
    }
}