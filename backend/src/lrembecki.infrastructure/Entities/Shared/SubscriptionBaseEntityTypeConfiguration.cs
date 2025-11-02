using lrembecki.core.Entities;
using lrembecki.core.subscription.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Shared;

internal class SubscriptionBaseEntityTypeConfiguration<T> : BaseEntityTypeConfiguration<T> where T : SubscriptionBaseEntity
{
    public override void Configure(EntityTypeBuilder<T> builder)
    {
        base.Configure(builder);
        builder.HasOne<SubscriptionEntity>().WithMany().HasForeignKey(e => e.SubscriptionId);
    }
}
