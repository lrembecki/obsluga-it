namespace lrembecki.obsluga_it.application.Contracts.ViewModels;

public record AccountVM(
    UserVM User,
    SubscriptionVM Subscription,
    List<string> Permissions,
    string Token,
    DateTime Created,
    DateTime Expires,
    bool IsAuthenticated);
