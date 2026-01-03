using lrembecki.core.storage;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.storage.Entities;

internal class StorageEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<StorageEntity>
{
    public override void Configure(EntityTypeBuilder<StorageEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Storage");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Filename).IsRequired().HasMaxLength(500);
        builder.Property(e => e.BlobUrl).IsRequired().HasMaxLength(500);
        builder.Property(e => e.BlobPath).IsRequired().HasMaxLength(500);

        builder.Navigation(e => e.Image).AutoInclude();
        builder.Navigation(e => e.File).AutoInclude();
    }
}
