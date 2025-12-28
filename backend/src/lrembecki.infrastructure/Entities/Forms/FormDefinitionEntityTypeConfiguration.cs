using lrembecki.core.forms.FormDefinitions;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Forms;

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
            fb.HasKey(ff => new { ff.FormDefinitionId, ff.FieldName });
            fb.Property(e => e.FieldName).HasMaxLength(200).IsRequired();
            fb.Property(e => e.FieldType).HasMaxLength(100).IsRequired();
        });

        builder.HasOne(e => e.Notification).WithMany().HasForeignKey(e => e.NotificationId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(e => e.Fields).AutoInclude();
        builder.Navigation(e => e.Notification).AutoInclude();
    }
}
