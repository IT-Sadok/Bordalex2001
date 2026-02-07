namespace Domain.Entities;

public class Host
{
    public Guid Id { get; set; }
    public string ExternalId { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<Apartment> Apartments { get; set; } = [];
}
