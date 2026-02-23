using lrembecki.core.trotamundos.Trips.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripRequirementEntityTypeConfiguration : IEntityTypeConfiguration<TripRequirementEntity>
{
    public void Configure(EntityTypeBuilder<TripRequirementEntity> builder)
    {
        builder.ToTable("TrotamundosTripRequirement");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.Requirements).HasForeignKey(e => e.TripId);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
