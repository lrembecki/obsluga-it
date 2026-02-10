using lrembecki.core.trotamundos.IndividualTrips;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class IndividualTripEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<IndividualTripEntity>
{
    public override void Configure(EntityTypeBuilder<IndividualTripEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosIndividualTrip");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.HasMany(e => e.Items)
            .WithOne()
            .HasForeignKey(e => e.IndividualTripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.Steps)
            .WithOne()
            .HasForeignKey(e => e.IndividualTripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Items).AutoInclude();
        builder.Navigation(e => e.Steps).AutoInclude();
    }
}
