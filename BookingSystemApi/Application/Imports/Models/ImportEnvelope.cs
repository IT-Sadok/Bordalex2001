using Application.Imports.Models.DTOs;

namespace Application.Imports.Models;
public class ImportEnvelope
{
    public HostImportDto Host { get; set; } = null!;
    public List<ApartmentImportDto> Apartments { get; set; } = [];
}
