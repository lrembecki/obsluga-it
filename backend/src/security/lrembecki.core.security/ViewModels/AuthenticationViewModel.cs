namespace lrembecki.core.security.ViewModels;

public record AuthenticationViewModel(
    Guid UserId,
    Guid SubscriptionId,
    string[] Permissions,
    string jwtToken
);
