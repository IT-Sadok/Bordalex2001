namespace Infrastructure.Imports;

public class ApartmentImportDto
{
    public string ExternalId { get; set; } = null!;
    public string HostExternalId { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public bool IsAvailable { get; set; }
}
