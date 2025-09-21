namespace BookingSystem;

public class Program
{
    public static void Main(string[] args)
    {
        var hosts = CreateHosts();
        DisplayHosts(hosts);
        var selectedHost = DisplayHostById(hosts);
        if (selectedHost != null)
        {
            DisplayApartments(selectedHost);
        }
    }

    private static List<Host> CreateHosts()
    {
        return new List<Host>
        {
            new() {
                Id = 1,
                Name = "Host1",
                Apartments =
                [
                    new Apartment { Id = 1, Name = "Apartment1" },
                    new Apartment { Id = 2, Name = "Apartment2" },
                    new Apartment { Id = 3, Name = "Apartment3" },
                    new Apartment { Id = 4, Name = "Apartment4" },
                    new Apartment { Id = 5, Name = "Apartment5" }
                ]
            },
            new() {
                Id = 2,
                Name = "Host2",
                Apartments =
                [
                    new Apartment { Id = 6, Name = "Apartment6" },
                    new Apartment { Id = 7, Name = "Apartment7" },
                    new Apartment { Id = 8, Name = "Apartment8" },
                    new Apartment { Id = 9, Name = "Apartment9" },
                    new Apartment { Id = 10, Name = "Apartment10" }
                ]
            },
            new() {
                Id = 3,
                Name = "Host3",
                Apartments =
                [
                    new Apartment { Id = 11, Name = "Apartment11" },
                    new Apartment { Id = 12, Name = "Apartment12" },
                    new Apartment { Id = 13, Name = "Apartment13" },
                    new Apartment { Id = 14, Name = "Apartment14" },
                    new Apartment { Id = 15, Name = "Apartment15" }
                ]
            },
            new() {
                Id = 4,
                Name = "Host4",
                Apartments =
                [
                    new Apartment { Id = 16, Name = "Apartment16" },
                    new Apartment { Id = 17, Name = "Apartment17" },
                    new Apartment { Id = 18, Name = "Apartment18" },
                    new Apartment { Id = 19, Name = "Apartment19" },
                    new Apartment { Id = 20, Name = "Apartment20" }
                ]
            },
            new() {
                Id = 5,
                Name = "Host5",
                Apartments =
                [
                    new Apartment { Id = 21, Name = "Apartment21" },
                    new Apartment { Id = 22, Name = "Apartment22" },
                    new Apartment { Id = 23, Name = "Apartment23" },
                    new Apartment { Id = 24, Name = "Apartment24" },
                    new Apartment { Id = 25, Name = "Apartment25" }
                ]
            }
        };
    }

    private static void DisplayHosts(List<Host> hosts)
    {
        Console.WriteLine("List of hosts:\n");
        foreach (var host in hosts)
        {
            Console.WriteLine($"Host ID: {host.Id}, Name: {host.Name}");
        }
    }

    private static Host? DisplayHostById(List<Host> hosts)
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
    }

    private static void DisplayApartments(Host host)
    {
        Console.WriteLine($"\nApartments for {host.Name}\n");
        foreach (var apartment in host.Apartments)
        {
            Console.WriteLine($"Apartment ID: {apartment.Id}, Name: {apartment.Name}");
        }
    }
}