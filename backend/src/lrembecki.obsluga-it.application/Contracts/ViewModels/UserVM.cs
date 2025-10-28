using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record UserVM(
    Guid Id,
    string Email,
    List<SubscriptionVM> Subscriptions)
{
    internal static UserVM MapFromDomainEntity(UserEntity user)
    {
        return new UserVM(
            user.Id,
            user.Email.Address,
            user.UserSubscriptions.Where(e => e.IsActive)
                .Select(e => e.Subscription)
                .Select(SubscriptionVM.MapFromDomainEntity)
                .ToList()
        );
    }
}
