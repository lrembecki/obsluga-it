using lrembecki.shared.domain.Entities;

namespace lrembecki.shared.application.ViewModels;

public record SubscriptionVM(Guid Id, string Name)
{
    public static SubscriptionVM MapFromDomainEntity(SubscriptionEntity subscription)
    {
        return new SubscriptionVM(
            subscription.Id,
            subscription.Name
        );
    }
}