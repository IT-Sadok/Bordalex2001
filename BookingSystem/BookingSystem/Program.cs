namespace BookingSystem;

public class Program
{
    private static readonly List<Host> hosts = [];
    private static int id;

    public static void Main(string[] args)
    {
        var hosts = CreateHosts();
        DisplayHosts(hosts);
        /*var selectedHost = DisplayHostById(hosts);
        if (selectedHost != null)
        {
            DisplayApartments(selectedHost);
        }*/
    }

    private static List<Host> CreateHosts()
    {
        Console.Write("Enter the number of hosts to create: ");
        if (int.TryParse(Console.ReadLine(), out var numberOfHosts) && numberOfHosts > 0)
        {
            for (var i = 0; i < numberOfHosts; i++)
            {
                id++;
                Console.Write($"Enter the name for host {id}: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    hosts.Add(new Host { Id = id, Name = name });
                }
                else
                {
                    Console.WriteLine("Host name shouldn't be empty. Please try again.");
                    i--;
                }
            }
            return hosts;
        }
        Console.WriteLine("Invalid input. Please enter a positive integer.");
        return CreateHosts(); // Retry if input is invalid
    }

    private static void DisplayHosts(List<Host> hosts)
    {
        Console.WriteLine("List of hosts:\n");
        foreach (var host in hosts)
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
            var hostToEdit = hosts.Find(h => h.Id == id);
            if (hostToEdit != null)
            {
                Console.Write("Enter the new name for the host: ");
                var newName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newName))
                {
                    hostToEdit.Name = newName;
                    Console.WriteLine("Host updated successfully!");
                }
                else
                {
                    Console.WriteLine("Host name shouldn't be empty. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Host not found");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid host ID.");
        }
    }

    private static void DeleteHost(List<Host> hosts)
    {
        Console.Write("Enter the host ID you want to delete: ");
        if (int.TryParse(Console.ReadLine(), out var id))
        {
            var hostToDelete = hosts.Find(h => h.Id == id);
            if (hostToDelete != null)
            {
                hosts.Remove(hostToDelete);
                Console.WriteLine("Host deleted successfully!");
            }
            else
            {
                Console.WriteLine("Host not found");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid host ID.");
        }
    }
}