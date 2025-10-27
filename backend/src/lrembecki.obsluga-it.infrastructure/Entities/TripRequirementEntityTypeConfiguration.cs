using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripRequirementEntityTypeConfiguration : IEntityTypeConfiguration<TripRequirementEntity>
{
    public void Configure(EntityTypeBuilder<TripRequirementEntity> builder)
    {
        builder.ToTable("TripRequirement");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(200);
    }
}
