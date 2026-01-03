namespace lrembecki.core.shared.Subscriptions;

public record SubscriptionVM(Guid Id, string Name)
{
    public static SubscriptionVM Map(SubscriptionEntity subscription)
    {
        if (subscription == null) return null!;

        return new SubscriptionVM(
            subscription.Id,
            subscription.Name
        );
    }
}