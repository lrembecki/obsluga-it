using lrembecki.core.trotamundos.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripHighlightEntityTypeConfiguration : IEntityTypeConfiguration<TripHighlightEntity>
{
    public void Configure(EntityTypeBuilder<TripHighlightEntity> builder)
    {
        builder.HasKey(x => new { x.TripId, x.Order });
        builder.ToTable("TrotamundosTripHighlight");

        builder.HasOne<TripEntity>().WithMany(e => e.Highlights).HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<HighlightEntity>()
            .WithMany()
            .HasForeignKey(e => e.HighlightId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
