using lrembecki.core.account.AccountSubscriptions;
using lrembecki.core.account.PermissionGroups;
using lrembecki.infrastructure.shared;
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

        builder.HasOne(e => e.Account)
            .WithMany()
            .HasForeignKey(e => e.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(e => e.Subscription)
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

        builder.Navigation(e => e.Account).AutoInclude();
        builder.Navigation(e => e.Subscription).AutoInclude();
        builder.Navigation(e => e.PermissionGroups).AutoInclude();
    }
}
