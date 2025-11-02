using lrembecki.core.storage.Entities;
using lrembecki.core.trotamundos.Entitites;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class TripImageEntityTypeConfiguration : IEntityTypeConfiguration<TripImageEntity>
{
    public void Configure(EntityTypeBuilder<TripImageEntity> builder)
    {
        builder.ToTable("TripImage");
        builder.HasKey(e => new { e.TripId, e.Order });

        builder.HasOne<TripEntity>().WithMany(e => e.Images).HasForeignKey(e => e.TripId);
        builder.HasOne<StorageEntity>().WithOne().HasForeignKey<TripImageEntity>(e => e.ImageId).OnDelete(DeleteBehavior.Restrict);
    }
}
