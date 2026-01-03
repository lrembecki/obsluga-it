using lrembecki.core.shared.Subscriptions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.shared;

public class SubscriptionBaseEntityTypeConfiguration<T> : BaseEntityTypeConfiguration<T> where T : SubscriptionBaseEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.HasOne<SubscriptionEntity>().WithMany().HasForeignKey(e => e.SubscriptionId);
    }
}
