using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Apartment> Apartments { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<ImportJob> ImportJobs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Apartment>().Property(a => a.PricePerNight).HasColumnType("decimal(18,2)");
        builder.Entity<Booking>().Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Entity<Apartment>().HasOne<AppUser>().WithMany().HasForeignKey(a => a.HostId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Booking>().HasOne<AppUser>().WithMany().HasForeignKey(b => b.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Booking>().HasOne<Apartment>().WithMany().HasForeignKey(b => b.ApartmentId).OnDelete(DeleteBehavior.Cascade);
        builder.Entity<ImportJob>().Property(j => j.Status).HasConversion<string>();
        builder.Entity<AppUser>().HasIndex(u => u.ExternalId).IsUnique();
        builder.Entity<Apartment>().HasIndex(a => a.ExternalId).IsUnique();
        builder.Entity<AppUser>().Property(u => u.ExternalId).IsRequired();
        builder.Entity<Apartment>().Property(a => a.ExternalId).IsRequired();
        builder.Entity<Apartment>().Property(a => a.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Entity<Booking>().Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
