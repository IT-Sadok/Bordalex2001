namespace Application.Features.Imports.DTOs;

public sealed class ImportEnvelopeDto
{
    public HostImportDto Host { get; set; } = null!;
    public IReadOnlyCollection<ApartmentImportDto> Apartments { get; set; } = [];
}
