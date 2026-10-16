namespace Application.Features.Apartments.DTOs;

public class ApartmentListItemDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Address { get; set; } = null!;
    public decimal PricePerNight { get; set; }
    public string? PrimaryImageUrl { get; set; }
}
