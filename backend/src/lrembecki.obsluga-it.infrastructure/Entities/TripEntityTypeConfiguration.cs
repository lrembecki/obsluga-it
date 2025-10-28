using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<TripEntity>
{
    public override void Configure(EntityTypeBuilder<TripEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Trip");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Title)
            .IsRequired().HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired().HasMaxLength(2048);

        builder.HasMany(e => e.Images).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.Schedules).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.PriceIncludes).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(e => e.PaymentSchedules).WithOne().HasForeignKey(e => e.TripId).OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Highlights).WithMany().UsingEntity<Dictionary<string, object>>("TripHighlight",
            j => j.HasOne<HighlightEntity>().WithMany().HasForeignKey("HighlightId").OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<TripEntity>().WithMany().HasForeignKey("TripId").OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.ToTable("TripHighlight");
                j.HasKey("HighlightId", "TripId");
            });

        builder.HasMany(e => e.Advantages).WithMany().UsingEntity<Dictionary<string, object>>("TripAdvantage",
            j => j.HasOne<AdvantageEntity>().WithMany().HasForeignKey("AdvantageId").OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<TripEntity>().WithMany().HasForeignKey("TripId").OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.ToTable("TripAdvantage");
                j.HasKey("AdvantageId", "TripId");
            });
    }
}
