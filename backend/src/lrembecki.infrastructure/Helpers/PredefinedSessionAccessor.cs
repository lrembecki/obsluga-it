using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public class PredefinedSessionAccessor(Guid subscriptionid) : ISessionAccessor
{
    private Guid? _overrideSubscriptionId = null!;

    public bool IsAuthenticated => true;

    public string? Email => "automated@lrembecki.pl";

    public Guid? SubscriptionId => _overrideSubscriptionId ?? subscriptionid;

    public Guid? UserId => Guid.NewGuid();

    void ISessionAccessor.OverrideSubscriptionId(Guid? subscriptionId)
        => _overrideSubscriptionId = subscriptionId;
}
