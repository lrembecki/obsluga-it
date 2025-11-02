using lrembecki.core.subscription.Entities;

namespace lrembecki.core.subscription.ViewModels;

public record SubscriptionVM(Guid Id, string Name)
{
    public static SubscriptionVM Map(SubscriptionEntity subscription)
    {
        return new SubscriptionVM(
            subscription.Id,
            subscription.Name
        );
    }
}