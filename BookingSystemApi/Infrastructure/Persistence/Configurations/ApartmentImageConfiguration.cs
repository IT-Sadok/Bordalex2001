using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public sealed class ApartmentImageConfiguration : IEntityTypeConfiguration<ApartmentImage>
{
    public void Configure(EntityTypeBuilder<ApartmentImage> builder)
    {
        builder.HasKey(ai => ai.Id);
        builder.Property(ai => ai.ImageUrl).IsRequired().HasMaxLength(2048);
        builder.HasOne(ai => ai.Apartment).WithMany(ai => ai.Images).HasForeignKey(ai => ai.ApartmentId).OnDelete(DeleteBehavior.Cascade);
        builder.HasIndex(ai => ai.ApartmentId);
        builder.HasIndex(ai => ai.ApartmentId).IsUnique().HasFilter("\"IsPrimary\" = TRUE");
        builder.HasIndex(ai => new { ai.ApartmentId, ai.SortOrder });
    }
}
