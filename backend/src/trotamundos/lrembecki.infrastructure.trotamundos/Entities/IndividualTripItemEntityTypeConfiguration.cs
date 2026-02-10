using lrembecki.core.trotamundos.IndividualTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class IndividualTripItemEntityTypeConfiguration : IEntityTypeConfiguration<IndividualTripItemEntity>
{
    public void Configure(EntityTypeBuilder<IndividualTripItemEntity> builder)
    {
        builder.HasKey(e => new { e.IndividualTripId, e.Order });
        builder.ToTable("TrotamundosIndividualTripItem");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(e => e.Uom)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasOne(e => e.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
