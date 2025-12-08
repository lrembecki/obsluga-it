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

        builder.ToTable("Forms");
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.OwnsMany(e => e.Fields, fb =>
        {
            fb.ToTable("FormFields");
            fb.WithOwner().HasForeignKey("FormId");
            fb.HasKey("FormId", "Name");
            fb.Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(200);
            fb.Property(f => f.Value)
                .IsRequired(false)
                .HasMaxLength(5000);
        });

        builder.HasIndex(e => e.Name).IsUnique(false);
    }
}
