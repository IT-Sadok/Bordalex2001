using Domain.Entities.Enums;

namespace Domain.Entities;

public class ImportJob
{
    public Guid Id { get; set; }
    public string FilePath { get; set; } = null!;
    public ImportStatus Status { get; set; }
    public int TotalRecords { get; set; }
    public int ProcessedRecords { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
