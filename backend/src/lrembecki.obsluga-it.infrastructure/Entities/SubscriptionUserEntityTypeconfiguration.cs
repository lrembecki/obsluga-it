using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.infrastructure.Entities.Templates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class SubscriptionUserEntityTypeconfiguration : SubscriptionBaseEntityTypeConfiguration<SubscriptionUserEntity>
{
    public override void Configure(EntityTypeBuilder<SubscriptionUserEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("SubscriptionUser");
        builder.HasKey(e => new { e.SubscriptionId, e.UserId });

        builder.Property(e => e.IsActive);
        builder.Property(e => e.IsDefault);

        builder.HasOne(e => e.User)
            .WithMany(e => e.UserSubscriptions)
            .HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Subscription)
            .WithMany()
            .HasForeignKey(e => e.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
