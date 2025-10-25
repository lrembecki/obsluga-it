using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record SubscriptionVM(Guid Id, string Name)
{
    internal static SubscriptionVM MapFromDomainEntity(Subscription subscription)
    {
        return new SubscriptionVM(
            subscription.Id,
            subscription.Name
        );
    }
}