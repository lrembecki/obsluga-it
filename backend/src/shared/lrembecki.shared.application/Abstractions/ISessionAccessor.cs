namespace lrembecki.shared.application.Abstractions;

public interface ISessionAccessor
{
    bool IsAuthenticated { get; }
    string? Email { get; }
    Guid? SubscriptionId { get; }
    Guid? UserId { get; }
}
