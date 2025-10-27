using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripPaymentScheduleEntityTypeConfiguration : IEntityTypeConfiguration<TripPaymentScheduleEntity>
{
    public void Configure(EntityTypeBuilder<TripPaymentScheduleEntity> builder)
    {
        builder.ToTable("TripPaymentSchedule");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany().HasForeignKey(e => e.TripId);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Price)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
