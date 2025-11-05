using lrembecki.core.trotamundos.Entitites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripPaymentScheduleEntityTypeConfiguration : IEntityTypeConfiguration<TripPaymentScheduleEntity>
{
    public void Configure(EntityTypeBuilder<TripPaymentScheduleEntity> builder)
    {
        builder.ToTable("TrotamundosTripPaymentSchedule");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.PaymentSchedules).HasForeignKey(e => e.TripId);

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
