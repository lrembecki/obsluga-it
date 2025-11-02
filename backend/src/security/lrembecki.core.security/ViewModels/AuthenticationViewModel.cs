using lrembecki.core.account.ViewModels;
using lrembecki.core.subscription.ViewModels;

namespace lrembecki.core.security.ViewModels;

public record AuthenticationViewModel(
    string Token,
    string[] Permissions,
    DateTime Expires,
    DateTime Created,
    AccountVM User,
    SubscriptionVM Subscription,
    bool IsAuthenticated
);
