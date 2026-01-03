using lrembecki.infrastructure.shared;
using lrembecki.core.forms.FormDefinitions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.forms.Entities;

internal class FormDefinitionEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<FormDefinitionEntity>
{
    public override void Configure(EntityTypeBuilder<FormDefinitionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("FormDefinition");
        builder.HasKey(e => e.Id);
        builder.OwnsMany(e => e.Fields, fb =>
        {
            fb.ToTable("FormDefinitionField");
            fb.WithOwner().HasForeignKey(e => e.FormDefinitionId);

            fb.HasKey(ff => new { ff.FormDefinitionId, ff.FieldName });
            fb.Property(e => e.FieldName).HasMaxLength(200).IsRequired();
            fb.Property(e => e.FieldType).HasMaxLength(100).IsRequired();
        });

        builder.OwnsMany(e => e.EmailNotificationFieldMapping, fb =>
        {
            fb.ToTable("FormDefinitionEmailNotificationMapping");
            fb.WithOwner().HasForeignKey(e => e.FormDefinitionId);

            fb.HasKey(ff => new { ff.FormDefinitionId, ff.FormDefinitionFieldName, ff.EmailTemplateFieldName });
            fb.Property(e => e.EmailTemplateFieldName).IsRequired(true).HasMaxLength(150);
            fb.Property(e => e.FormDefinitionFieldName).IsRequired(true).HasMaxLength(150);
        });

        builder.HasOne(e => e.Notification)
            .WithMany()
            .HasForeignKey(e => e.NotificationId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Fields).AutoInclude();
        builder.Navigation(e => e.Notification).AutoInclude();
    }
}
