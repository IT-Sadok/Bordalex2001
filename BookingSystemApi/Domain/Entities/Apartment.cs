namespace Domain.Entities;

public class Apartment
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public Guid HostId { get; set; }
    public Host Host { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}
