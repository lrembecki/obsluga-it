using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public class PredefinedSessionAccessor : ISessionAccessor
{
    private Guid? _overrideSubscriptionId = null!;

    public bool IsAuthenticated => true;

    public string? Email => "automated@lrembecki.pl";

    public Guid? SubscriptionId => _overrideSubscriptionId;

    public Guid? UserId => Guid.NewGuid();

    void ISessionAccessor.OverrideSubscriptionId(Guid? subscriptionId)
        => _overrideSubscriptionId = subscriptionId;
}
