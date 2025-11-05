using lrembecki.core.trotamundos.Entitites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripRequirementEntityTypeConfiguration : IEntityTypeConfiguration<TripRequirementEntity>
{
    public void Configure(EntityTypeBuilder<TripRequirementEntity> builder)
    {
        builder.ToTable("TrotamundosTripRequirement");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.Requirements).HasForeignKey(e => e.TripId);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200);
    }
}
