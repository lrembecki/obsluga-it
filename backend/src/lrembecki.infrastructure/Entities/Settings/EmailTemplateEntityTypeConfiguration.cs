using lrembecki.core.settings.Entities;
using lrembecki.core.settings.ViewModels;
using lrembecki.infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Settings;

internal class EmailTemplateEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<EmailTemplateEntity>
{
    public override void Configure(EntityTypeBuilder<EmailTemplateEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("EmailTemplate");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.HasOne(e => e.TemplateHtml)
            .WithOne()
            .HasForeignKey<EmailTemplateEntity>(e => e.TemplateHtmlId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Fields).AutoInclude();
        builder.Navigation(e => e.TemplateHtml).AutoInclude();
    }
}