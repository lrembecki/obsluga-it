using lrembecki.core.trotamundos.Pages.AboutUs;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.trotamundos.Entities;

internal class AboutUsEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<AboutUsEntity>
{
    public override void Configure(EntityTypeBuilder<AboutUsEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("TrotamundosPagesAboutUs");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(5000);

        builder.HasOne(e => e.Image)
            .WithMany()
            .HasForeignKey(e => e.ImageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Items).WithOne()
            .HasForeignKey(e => e.AboutUsId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(e => e.Image).AutoInclude();
        builder.Navigation(e => e.Items).AutoInclude();
        builder.Navigation(e => e.Persons).AutoInclude();
    }
}
