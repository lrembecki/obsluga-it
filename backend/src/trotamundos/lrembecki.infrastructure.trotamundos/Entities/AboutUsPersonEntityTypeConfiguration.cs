using lrembecki.core.trotamundos.Pages.AboutUs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class AboutUsPersonEntityTypeConfiguration : IEntityTypeConfiguration<AboutUsPersonEntity>
{
    public void Configure(EntityTypeBuilder<AboutUsPersonEntity> builder)
    {
        builder.HasKey(e => new { e.AboutUsId, e.Order });
        builder.ToTable("TrotamundosPagesAboutUsPerson");

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasOne(e => e.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Image).AutoInclude();
    }
}
