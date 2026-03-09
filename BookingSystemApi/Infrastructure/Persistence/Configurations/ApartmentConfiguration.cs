using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
{
    public void Configure(EntityTypeBuilder<Apartment> builder)
    {
        builder.Property(a => a.PricePerNight).HasColumnType("decimal(18,2)");
        builder.HasIndex(a => a.ExternalId).IsUnique();
        builder.Property(a => a.ExternalId).IsRequired();
        builder.Property(a => a.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasOne<AppUser>().WithMany().HasForeignKey(a => a.HostId).OnDelete(DeleteBehavior.Restrict);
    }
}
