using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using lrembecki.shared.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class ImageBlobEntityTypeConfiguration : BlobBaseEntityTypeConfiguration<ImageBlobEntity>
{
    public override void Configure(EntityTypeBuilder<ImageBlobEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("ImageBlob");
        builder.HasKey(a => a.Id);

        builder.Property(e => e.DisplayName)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.HasMany(e => e.Tags).WithMany().UsingEntity<Dictionary<string, object>>("ImageBlobTag",
            j => j.HasOne<TagEntity>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<ImageBlobEntity>().WithMany().HasForeignKey("ImageBlobId").OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.ToTable("ImageBlobTag");
                j.HasKey("ImageBlobId", "TagId");
            });
    }
}
