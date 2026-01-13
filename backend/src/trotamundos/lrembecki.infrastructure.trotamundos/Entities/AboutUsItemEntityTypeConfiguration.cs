using lrembecki.core.trotamundos.Pages.AboutUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class AboutUsItemEntityTypeConfiguration : IEntityTypeConfiguration<AboutUsItemEntity>
{
    public void Configure(EntityTypeBuilder<AboutUsItemEntity> builder)
    {
        builder.HasKey(e => new { e.AboutUsId, e.Order });
        builder.ToTable("TrotamundosPagesAboutUsItem");

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);
    }
}
