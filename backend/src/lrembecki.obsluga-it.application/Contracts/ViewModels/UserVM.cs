using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.ViewModels;

namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record UserVM(
    Guid Id,
    string Email,
    List<SubscriptionVM> Subscriptions)
{
    public static UserVM MapFromDomainEntity(UserEntity user)
    {
        return new UserVM(
            user.Id,
            user.Email,
            user.UserSubscriptions.Where(e => e.IsActive)
                .Select(e => e.Subscription)
                .Select(SubscriptionVM.MapFromDomainEntity)
                .ToList()
        );
    }
}
