namespace Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ApartmentId { get; set; } 
    public Guid ClientId { get; set; } 
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
}
