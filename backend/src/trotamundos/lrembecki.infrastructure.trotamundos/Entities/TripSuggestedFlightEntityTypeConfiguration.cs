using lrembecki.core.trotamundos.Trips.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripSuggestedFlightEntityTypeConfiguration : IEntityTypeConfiguration<TripSuggestedFlightEntity>
{
    public void Configure(EntityTypeBuilder<TripSuggestedFlightEntity> builder)
    {
        builder.ToTable("TrotamundosTripSuggestedFlight");
        builder.HasKey(e => new { e.TripId, e.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.SuggestedFlights).HasForeignKey(e => e.TripId);
        builder.HasOne(e => e.Image).WithOne().HasForeignKey<TripSuggestedFlightEntity>(e => e.ImageId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
