using lrembecki.core.trotamundos.IndividualTrips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class IndividualTripStepItemEntityTypeConfiguration : IEntityTypeConfiguration<IndividualTripStepItemEntity>
{
    public void Configure(EntityTypeBuilder<IndividualTripStepItemEntity> builder)
    {
        builder.HasKey(e => new { e.IndividualTripId, e.Order });
        builder.ToTable("TrotamundosIndividualTripStepItem");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);
    }
}
