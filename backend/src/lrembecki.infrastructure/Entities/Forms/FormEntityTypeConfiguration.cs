using lrembecki.core.forms.FormDefinitions;
using lrembecki.core.forms.Forms;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Forms;

internal class FormEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<FormEntity>
{
    public override void Configure(EntityTypeBuilder<FormEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Form");
        builder.HasKey(e => e.Id);

        builder.HasOne<FormDefinitionEntity>().WithMany()
            .HasForeignKey(e => e.FormDefinitionId).IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.OwnsMany(e => e.Fields, fb =>
        {
            fb.ToTable("FormField");
            fb.HasKey(ff => new { ff.FormId, ff.Name });

            fb.Property(ff => ff.Value).HasMaxLength(2000);
        });

        builder.Navigation(e => e.Fields).AutoInclude();
    }
}
