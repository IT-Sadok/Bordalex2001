using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Host> Hosts { get; set; }
    public DbSet<ImportJob> ImportJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Apartment>().Property(a => a.PricePerNight).HasColumnType("decimal(18,2)");
        builder.Entity<Booking>().Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Entity<Host>().HasMany(h => h.Apartments).WithOne(a => a.Host).HasForeignKey(a => a.HostId);
        builder.Entity<ImportJob>().Property(j => j.Status).HasConversion<string>();
    }
}
