using lrembecki.core.trotamundos.Highlights;
using lrembecki.core.trotamundos.Trips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripHighlightEntityTypeConfiguration : IEntityTypeConfiguration<TripHighlightEntity>
{
    public void Configure(EntityTypeBuilder<TripHighlightEntity> builder)
    {
        builder.HasKey(x => new { x.TripId, x.HighlightId });
        builder.ToTable("TrotamundosTripHighlight");

        builder.HasOne<TripEntity>().WithMany(e => e.Highlights).HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<HighlightEntity>()
            .WithMany()
            .HasForeignKey(e => e.HighlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
