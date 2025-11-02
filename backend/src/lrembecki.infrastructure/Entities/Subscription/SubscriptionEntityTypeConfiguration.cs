using lrembecki.core.subscription.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Subscription;

internal class SubscriptionEntityTypeConfiguration : BaseEntityTypeConfiguration<SubscriptionEntity>
{
    public override void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("Subscription");

        builder.Property(e => e.Name).HasMaxLength(250).IsRequired();

        builder.HasKey(e => e.Id);
        builder.HasIndex(e => new { e.Name }).IsUnique();
    }
}
