using lrembecki.core.trotamundos.Trips;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class TripImageEntityTypeConfiguration : IEntityTypeConfiguration<TripImageEntity>
{
    public void Configure(EntityTypeBuilder<TripImageEntity> builder)
    {
        builder.ToTable("TrotamundosTripImage");
        builder.HasKey(e => e.ImageId);

        builder.HasOne<TripEntity>().WithMany(e => e.Images).HasForeignKey(e => e.TripId);
        builder.HasOne(e => e.Image).WithOne().HasForeignKey<TripImageEntity>(e => e.ImageId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
