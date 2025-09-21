using System;
using System.Collections.Generic;

namespace BookingSystem;

class Program
{
    static void Main(string[] args)
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
        return new List<Host>()
        {
            new Host(1, "Host1")
            {
                Apartments = new List<Apartment>()
                {
                    new Apartment(1, "Apartment1"),
                    new Apartment(2, "Apartment2"),
                    new Apartment(3, "Apartment3"),
                    new Apartment(4, "Apartment4"),
                    new Apartment(5, "Apartment5"),
                }
            },
            new Host(2, "Host2")
            {
                Apartments = new List<Apartment>()
                {
                    new Apartment(6, "Apartment6"),
                    new Apartment(7, "Apartment7"),
                    new Apartment(8, "Apartment8"),
                    new Apartment(9, "Apartment9"),
                    new Apartment(10, "Apartment10"),
                }
            },
            new Host(3, "Host3")
            {
                Apartments = new List<Apartment>()
                {
                    new Apartment(11, "Apartment11"),
                    new Apartment(12, "Apartment12"),
                    new Apartment(13, "Apartment13"),
                    new Apartment(14, "Apartment14"),
                    new Apartment(15, "Apartment15"),
                }
            },
            new Host(4, "Host4")
            {
                Apartments = new List<Apartment>()
                {
                    new Apartment(16, "Apartment16"),
                    new Apartment(17, "Apartment17"),
                    new Apartment(18, "Apartment18"),
                    new Apartment(19, "Apartment19"),
                    new Apartment(20, "Apartment20"),
                }
            },
            new Host(5, "Host5")
            {
                Apartments = new List<Apartment>()
                {
                    new Apartment(21, "Apartment21"),
                    new Apartment(22, "Apartment22"),
                    new Apartment(23, "Apartment23"),
                    new Apartment(24, "Apartment24"),
                    new Apartment(25, "Apartment25"),
                }
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
                Console.WriteLine($"Host not found");
            }
            return host;
        }
        Console.WriteLine("Invalid input. Please enter a valid host ID.");
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