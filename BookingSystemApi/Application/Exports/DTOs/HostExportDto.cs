
namespace Application.Exports.DTOs;

public sealed class HostExportDto
{
    public string ExternalId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string DisplayName { get; set; } = null!;
    public IReadOnlyCollection<ApartmentExportDto> Apartments { get; set; } = [];
}
