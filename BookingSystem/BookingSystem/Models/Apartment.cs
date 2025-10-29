namespace BookingSystem.Models;

public class Apartment(int price)
{
    //public int Id { get; set; }
    //public string Name { get; set; }
    public int Price { get; set; } = price;
    private readonly Lock _lock = new();

    public void IncreasePriceUnsafely(int amount)
    {
        Price += amount;
    }

    public void IncreasePriceSafely(int amount)
    {
        lock (_lock)
        {
            Price += amount;
        }
    }
}
