namespace Domain.Entities;

public class Apartment
{
    public Guid Id { get; set; }
    public Guid HostId { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; } = true;
}
