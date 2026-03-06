namespace Application.Features.Imports.DTOs;

public sealed class HostImportDto
{
    public string ExternalId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public IReadOnlyCollection<ApartmentImportDto> Apartments { get; set; } = [];
}
