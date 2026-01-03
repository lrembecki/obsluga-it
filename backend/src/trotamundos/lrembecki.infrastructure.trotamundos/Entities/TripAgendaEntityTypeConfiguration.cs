using lrembecki.core.trotamundos.Trips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripAgendaEntityTypeConfiguration : IEntityTypeConfiguration<TripAgendaEntity>
{
    public void Configure(EntityTypeBuilder<TripAgendaEntity> builder)
    {
        builder.ToTable("TrotamundosTripAgenda");
        builder.HasKey(e => new { e.TripId, e.Order });

        builder.HasOne<TripEntity>()
            .WithMany(t => t.Agenda)
            .HasForeignKey(e => e.TripId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.Content).HasMaxLength(250).IsRequired();
    }
}
