using lrembecki.core.settings.Contacts;
using lrembecki.core.settings.EmailTemplates;
using lrembecki.infrastructure.shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.settings.Entities;

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

        builder.Property(e => e.Subject)
            .IsRequired(false)
            .HasMaxLength(500);

        builder.HasOne(e => e.TemplateHtml)
            .WithOne()
            .HasForeignKey<EmailTemplateEntity>(e => e.TemplateHtmlId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasMany(e => e.Contacts_to)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EmailTemplate_Contacts_To",
                e => e.HasOne<ContactEntity>().WithMany().HasForeignKey("ContactId").OnDelete(DeleteBehavior.Restrict),
                e => e.HasOne<EmailTemplateEntity>().WithMany().HasForeignKey("EmailTemplateId").OnDelete(DeleteBehavior.Restrict));

        builder.HasMany(e => e.Contacts_cc)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EmailTemplate_Contacts_Cc",
                e => e.HasOne<ContactEntity>().WithMany().HasForeignKey("ContactId").OnDelete(DeleteBehavior.Restrict),
                e => e.HasOne<EmailTemplateEntity>().WithMany().HasForeignKey("EmailTemplateId").OnDelete(DeleteBehavior.Restrict));

        builder.HasMany(e => e.Contacts_bcc)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "EmailTemplate_Contacts_Bcc",
                e => e.HasOne<ContactEntity>().WithMany().HasForeignKey("ContactId").OnDelete(DeleteBehavior.Restrict),
                e => e.HasOne<EmailTemplateEntity>().WithMany().HasForeignKey("EmailTemplateId").OnDelete(DeleteBehavior.Restrict));

        builder.Navigation(e => e.Fields).AutoInclude();
        builder.Navigation(e => e.TemplateHtml).AutoInclude();
        builder.Navigation(e => e.Contacts_to).AutoInclude();
        builder.Navigation(e => e.Contacts_cc).AutoInclude();
        builder.Navigation(e => e.Contacts_bcc).AutoInclude();
    }
}