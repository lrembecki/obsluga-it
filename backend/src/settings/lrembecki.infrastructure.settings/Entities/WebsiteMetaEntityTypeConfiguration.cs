using lrembecki.core.settings.Website;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class WebsiteMetaEntityTypeConfiguration : IEntityTypeConfiguration<WebsiteMetaEntity>
{
    public void Configure(EntityTypeBuilder<WebsiteMetaEntity> builder)
    {
        builder.ToTable("WebsiteMeta");

        builder.HasKey(e => e.WebsiteId);
        builder.Property(e => e.Keywords).IsRequired(false).HasMaxLength(2000);
        builder.Property(e => e.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(e => e.Title).IsRequired(false).HasMaxLength(2000);

        builder.HasOne<WebsiteEntity>().WithOne(e => e.Meta)
            .HasForeignKey<WebsiteMetaEntity>(e => e.WebsiteId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.Image).WithOne()
            .HasForeignKey<WebsiteMetaEntity>(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
