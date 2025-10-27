using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripPriceIncludeEntityTypeConfiguration : IEntityTypeConfiguration<TripPriceIncludeEntity>
{
    public void Configure(EntityTypeBuilder<TripPriceIncludeEntity> builder)
    {
        builder.ToTable("TripPriceInclude");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
