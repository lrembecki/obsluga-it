using lrembecki.core.storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.storage.Entities;

internal class FileStorageEntityTypeConfiguration : IEntityTypeConfiguration<FileStorageEntity>
{
    public void Configure(EntityTypeBuilder<FileStorageEntity> builder)
    {
        builder.ToTable("FileStorage");
    
        builder.HasKey(e => e.StorageId);
        builder.HasOne<StorageEntity>().WithOne(e => e.File).HasForeignKey<FileStorageEntity>(e => e.StorageId).OnDelete(DeleteBehavior.Cascade);

        builder.Property(e => e.DisplayName).IsRequired().HasMaxLength(500);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(5000);
    }
}
