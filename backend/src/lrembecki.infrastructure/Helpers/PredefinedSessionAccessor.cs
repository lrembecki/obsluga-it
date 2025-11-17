using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public class PredefinedSessionAccessor(Guid subscriptionid) : ISessionAccessor
{
    public bool IsAuthenticated => true;

    public string? Email => "automated@lrembecki.pl";

    public Guid? SubscriptionId => subscriptionid;

    public Guid? UserId => Guid.NewGuid();
}
