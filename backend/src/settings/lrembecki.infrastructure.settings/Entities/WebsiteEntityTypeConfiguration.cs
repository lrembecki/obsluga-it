using lrembecki.core.settings.Website;
using lrembecki.infrastructure.shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

internal class WebsiteEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<WebsiteEntity>
{
    public override void Configure(EntityTypeBuilder<WebsiteEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Website");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(e => e.Company)
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Meta).AutoInclude();
    }
}
