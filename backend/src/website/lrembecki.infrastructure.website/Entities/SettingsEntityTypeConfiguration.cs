using lrembecki.core.website.Settings;
using lrembecki.infrastructure.shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.website.Entities;

internal class SettingsEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<SettingsEntity>
{
    public override void Configure(EntityTypeBuilder<SettingsEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("WebsiteSettings");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.PrimaryColor)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(x => x.SecondaryColor)
            .IsRequired()
            .HasMaxLength(7);

        builder.Property(x => x.FontFamily)
                        .IsRequired()
                        .HasMaxLength(100);

        builder.Property(x => x.FontSize)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasOne(e => e.FontFile)
            .WithOne()
            .HasForeignKey<SettingsEntity>(e => e.FontFileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
