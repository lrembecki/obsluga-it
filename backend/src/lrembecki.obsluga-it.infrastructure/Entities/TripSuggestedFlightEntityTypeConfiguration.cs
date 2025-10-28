using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripSuggestedFlightEntityTypeConfiguration : IEntityTypeConfiguration<TripSuggestedFlightEntity>
{
    public void Configure(EntityTypeBuilder<TripSuggestedFlightEntity> builder)
    {
        builder.ToTable("TripSuggestedFlight");
        builder.HasKey(e => new { e.TripId, e.Order });

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId);
        builder.HasOne(e => e.Image).WithOne().HasForeignKey<TripSuggestedFlightEntity>(e => e.ImageId).OnDelete(DeleteBehavior.Restrict);
    }
}
