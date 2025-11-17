using lrembecki.core.trotamundos.Entitites;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Trotamundos;

internal class FileEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<FileEntity>
{
    public override void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosFile");
        builder.HasKey(x => x.Id);

        builder.Property(e => e.Group).IsRequired(false).HasMaxLength(100);
        builder.Property(e => e.DisplayName).IsRequired(false).HasMaxLength(150);
        builder.Property(e => e.Description).IsRequired(false).HasMaxLength(5000);

        builder.HasOne(e => e.Storage).WithOne().HasForeignKey<FileEntity>(e => e.StorageId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Storage).AutoInclude();
    }
}
