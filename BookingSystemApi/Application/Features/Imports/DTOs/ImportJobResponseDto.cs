namespace Application.Features.Imports.DTOs;

public sealed class ImportJobResponseDto
{
    public Guid JobId { get; set; }
    public string Status { get; set; } = null!;
}
