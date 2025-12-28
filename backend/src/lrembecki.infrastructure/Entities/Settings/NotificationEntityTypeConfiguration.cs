using lrembecki.core.settings.Entities;
using lrembecki.infrastructure.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Settings;

internal class NotificationEntityTypeConfiguration : SubscriptionBaseEntityTypeConfiguration<NotificationEntity>
{
    public override void Configure(EntityTypeBuilder<NotificationEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Notification");
        builder.HasKey(n => n.Id);

        builder.OwnsOne(e => e.Email, owned =>
        {
            owned.WithOwner().HasForeignKey("NotificationId");
            owned.ToTable("NotificationEmail");

            owned.HasOne(e => e.Email).WithMany().HasForeignKey(e => e.EmailId).OnDelete(DeleteBehavior.Restrict);
            owned.HasOne(e => e.EmailTemplate).WithMany().HasForeignKey(e => e.EmailTemplateId).OnDelete(DeleteBehavior.Restrict);

            owned.Navigation(e => e.Email).AutoInclude();
            owned.Navigation(e => e.EmailTemplate).AutoInclude();
        });

        builder.Navigation(e => e.Email).AutoInclude();
    }
}
