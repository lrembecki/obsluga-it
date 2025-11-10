using lrembecki.core.trotamundos.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripHighlightEntityTypeConfiguration : IEntityTypeConfiguration<TripHighlightEntity>
{
    public void Configure(EntityTypeBuilder<TripHighlightEntity> builder)
    {
        builder.HasKey(x => x.TripId);
        builder.ToTable("TrotamundosTripHighlight");

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Highlight)
            .WithMany()
            .HasForeignKey(e => e.HighlightId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Highlight).AutoInclude();
    }
}
