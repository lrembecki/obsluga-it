using lrembecki.core.account.Accounts;
using lrembecki.core.shared.Subscriptions;

namespace lrembecki.core.security;

public record AuthenticationViewModel(
    string Token,
    string[] Permissions,
    DateTime Expires,
    DateTime Created,
    AccountVM User,
    SubscriptionVM Subscription,
    List<SubscriptionVM> Subscriptions,
    bool IsAuthenticated
);
