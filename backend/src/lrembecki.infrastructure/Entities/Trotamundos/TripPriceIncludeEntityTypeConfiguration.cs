using lrembecki.core.trotamundos.Entitites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripPriceIncludeEntityTypeConfiguration : IEntityTypeConfiguration<TripPriceIncludeEntity>
{
    public void Configure(EntityTypeBuilder<TripPriceIncludeEntity> builder)
    {
        builder.ToTable("TripPriceInclude");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.PriceIncludes).HasForeignKey(e => e.TripId);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
