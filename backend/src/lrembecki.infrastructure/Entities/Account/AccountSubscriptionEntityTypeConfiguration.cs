using lrembecki.core.account.Entities;
using lrembecki.core.subscription.Entities;
using lrembecki.infrastructure.Entities.Shared;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace lrembecki.infrastructure.Entities.Account;

internal class AccountSubscriptionEntityTypeConfiguration : BaseEntityTypeConfiguration<AccountSubscriptionEntity>
{
    public override void Configure(EntityTypeBuilder<AccountSubscriptionEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("AccountSubscription");
        
        builder.HasKey(e => e.Id);

        builder.HasOne<AccountEntity>()
            .WithMany()
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<SubscriptionEntity>()
            .WithMany()
            .HasForeignKey(e => e.SubscriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(e => e.PermissionGroups).WithMany().UsingEntity<Dictionary<string, string>>("AccountSubscriptionPermissionGroup",
            j => j.HasOne<PermissionGroupEntity>().WithMany().HasForeignKey("PermissionGroupId").OnDelete(DeleteBehavior.Cascade),
            j => j.HasOne<AccountSubscriptionEntity>().WithMany().HasForeignKey("AccountSubscriptionId").OnDelete(DeleteBehavior.Cascade),
            j =>
            {
                j.ToTable("AccountSubscriptionPermissionGroup");
                j.HasKey("AccountSubscriptionId", "PermissionGroupId");
            });
    }
}
