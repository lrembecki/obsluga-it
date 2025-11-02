using lrembecki.core.storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Storage;

internal class ImageStorageEntityTypeConfiguration : IEntityTypeConfiguration<ImageStorageEntity>
{
    public void Configure(EntityTypeBuilder<ImageStorageEntity> builder)
    {
        builder.ToTable("ImageStorage");
        
        builder.HasKey(e => e.StorageId);
        builder.HasOne<StorageEntity>().WithOne(e => e.Image).HasForeignKey<ImageStorageEntity>(e => e.StorageId).OnDelete(DeleteBehavior.Cascade);
        
        builder.Property(e => e.Width).IsRequired();
        builder.Property(e => e.Height).IsRequired();
        builder.Property(e => e.DisplayName).IsRequired().HasMaxLength(500);
    }
}
