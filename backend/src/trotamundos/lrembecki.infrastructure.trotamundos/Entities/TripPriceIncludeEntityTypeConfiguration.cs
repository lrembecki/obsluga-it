using lrembecki.core.trotamundos.Trips.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripPriceIncludeEntityTypeConfiguration : IEntityTypeConfiguration<TripPriceIncludeEntity>
{
    public void Configure(EntityTypeBuilder<TripPriceIncludeEntity> builder)
    {
        builder.ToTable("TrotamundosTripPriceInclude");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.PriceIncludes).HasForeignKey(e => e.TripId);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
