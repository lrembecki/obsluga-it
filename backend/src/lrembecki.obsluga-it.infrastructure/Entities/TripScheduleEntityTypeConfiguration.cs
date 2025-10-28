using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class TripScheduleEntityTypeConfiguration : IEntityTypeConfiguration<TripScheduleEntity>
{
    public void Configure(EntityTypeBuilder<TripScheduleEntity> builder)
    {
        builder.ToTable("TripSchedule");
        builder.HasKey(x => new { x.TripId, x.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.Schedules).HasForeignKey(e => e.TripId);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Content)
            .IsRequired()
            .HasMaxLength(2048);
    }
}
