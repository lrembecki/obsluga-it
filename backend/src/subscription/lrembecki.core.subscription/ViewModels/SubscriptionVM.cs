using lrembecki.core.subscription.Entities;

namespace lrembecki.core.subscription.ViewModels;

public record SubscriptionVM(Guid Id, string Name)
{
    internal static SubscriptionVM MapFromDomainEntity(SubscriptionEntity subscription)
    {
        return new SubscriptionVM(
            subscription.Id,
            subscription.Name
        );
    }
}