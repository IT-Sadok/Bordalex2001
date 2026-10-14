namespace Domain.Entities;

public class ApartmentImage
{
    public Guid Id { get; set; }
    public Guid ApartmentId { get; set; }
    public string ImageUrl { get; set; } = null!;
    public bool IsPrimary { get; set; }
    public int SortOrder { get; set; }
    public Apartment Apartment { get; set; } = null!;
}
