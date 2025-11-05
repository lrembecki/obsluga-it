using lrembecki.core.account.Entities;

namespace lrembecki.core.account.ViewModels;

public record AccountSubscriptionVM(
    Guid Id,
    string Email,
    string Subscription,
    Guid SubscriptionId,
    bool IsActive,
    bool IsDefault,
    ICollection<PermissionGroupVM> PermissionGroups
)
{
    public static AccountSubscriptionVM Map(
        AccountSubscriptionEntity accountSubscriptionEntity
    )
    {
        if (accountSubscriptionEntity == null) return null!;

        return new AccountSubscriptionVM(
            accountSubscriptionEntity.Id,
            accountSubscriptionEntity.Account.Email,
            accountSubscriptionEntity.Subscription.Name,
            accountSubscriptionEntity.SubscriptionId,
            accountSubscriptionEntity.IsActive,
            accountSubscriptionEntity.IsDefault,
            accountSubscriptionEntity.PermissionGroups
                .Select(PermissionGroupVM.Map)
                .ToList()
        );
    }
}
