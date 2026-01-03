using lrembecki.core.trotamundos.Advantages;
using lrembecki.core.trotamundos.Trips;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<TripEntity>
{
    public override void Configure(EntityTypeBuilder<TripEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosTrip");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
            .IsRequired().HasMaxLength(200);

        builder.Property(e => e.Subtitle)
            .IsRequired(false).HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired(false).HasMaxLength(2048);

        builder.Property(e => e.SuggestedFlightNotes)
            .IsRequired(false).HasMaxLength(2048);

        builder.HasMany(e => e.Advantages).WithMany().UsingEntity<Dictionary<string, object>>("TrotamundosTripAdvantage",
            j => j.HasOne<AdvantageEntity>().WithMany().HasForeignKey("AdvantageId").OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<TripEntity>().WithMany().HasForeignKey("TripId").OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.ToTable("TrotamundosTripAdvantage");
                j.HasKey("AdvantageId", "TripId");
            });

        builder.HasMany(e => e.Highlights).WithOne().HasForeignKey(e => e.TripId);
        builder.HasMany(e => e.Images).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.PaymentSchedules).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.PriceIncludes).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.Requirements).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.Schedules).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.SuggestedFlights).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);

        // Navigation
        builder.Navigation(e => e.Agenda).AutoInclude();
        builder.Navigation(e => e.Advantages).AutoInclude();
        builder.Navigation(e => e.Highlights).AutoInclude();
        builder.Navigation(e => e.Images).AutoInclude();
        builder.Navigation(e => e.LoyalityPrograms).AutoInclude();
        builder.Navigation(e => e.Schedules).AutoInclude();
        builder.Navigation(e => e.PriceIncludes).AutoInclude();
        builder.Navigation(e => e.PaymentSchedules).AutoInclude();
        builder.Navigation(e => e.Requirements).AutoInclude();
        builder.Navigation(e => e.SuggestedFlights).AutoInclude();
    }
}
