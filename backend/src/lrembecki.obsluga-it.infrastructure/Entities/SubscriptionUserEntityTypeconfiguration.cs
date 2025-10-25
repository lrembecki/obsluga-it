using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.obsluga_it.infrastructure.Entities;

internal class SubscriptionUserEntityTypeconfiguration : IEntityTypeConfiguration<SubscriptionUser>
{
    public void Configure(EntityTypeBuilder<SubscriptionUser> builder)
    {
        builder.ToTable(nameof(SubscriptionUser));

        builder.HasKey(e => new { e.SubscriptionId, e.UserId });

        builder.Property(e => e.SubscriptionId);

        builder.Property(e => e.UserId)
            .IsRequired();

        builder.Property(e => e.IsActive);

        builder.Property(e => e.IsDefault);

        builder.HasOne(e => e.Subscription).WithMany(e => e.UserSubscriptions).HasForeignKey(e => e.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.User).WithMany(e => e.UserSubscriptions).HasForeignKey(e => e.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
