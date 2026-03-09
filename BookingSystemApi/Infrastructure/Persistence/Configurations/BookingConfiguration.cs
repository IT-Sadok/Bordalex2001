using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class BookingConfiguration : IEntityTypeConfiguration<Booking>
{

    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)");
        builder.Property(b => b.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasOne<AppUser>().WithMany().HasForeignKey(b => b.ClientId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Apartment>().WithMany().HasForeignKey(b => b.ApartmentId).OnDelete(DeleteBehavior.Cascade);
    }
}