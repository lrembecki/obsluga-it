using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripImageEntityTypeConfiguration : IEntityTypeConfiguration<TripImageEntity>
{
    public void Configure(EntityTypeBuilder<TripImageEntity> builder)
    {
        builder.ToTable("TripImage");
        builder.HasKey(e => new { e.TripId, e.Order });

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId);
        builder.HasOne(e => e.Image).WithOne().HasForeignKey<TripImageEntity>(e => e.ImageId);
    }
}
