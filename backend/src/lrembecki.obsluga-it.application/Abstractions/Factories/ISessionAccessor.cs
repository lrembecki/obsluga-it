namespace lrembecki.obsluga_it.application.Abstractions.Factories;

public interface ISessionAccessor
{
    bool IsAuthenticated { get; }
    string? Email { get; }
    Guid? SubscriptionId { get; }
    Guid? UserId { get; }
}
