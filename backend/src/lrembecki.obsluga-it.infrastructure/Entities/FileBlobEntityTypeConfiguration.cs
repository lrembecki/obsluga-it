using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using lrembecki.shared.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class FileBlobEntityTypeConfiguration : BlobBaseEntityTypeConfiguration<FileBlobEntity>
{
    public override void Configure(EntityTypeBuilder<FileBlobEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("FileBlob");
        builder.HasKey(a => a.Id);

        builder.Property(e => e.DisplayName)
            .IsRequired(false)
            .HasMaxLength(255);

        builder.Property(e => e.Description)
            .IsRequired(false)
            .HasMaxLength(1000);

        builder.HasOne(e => e.Group)
            .WithMany()
            .HasForeignKey(e => e.GroupId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
