using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public static class SessionContextExtension
{
    public static SessionContext CreateSessionContext<T>(this T session, Guid subscriptionId) where T : ISessionAccessor
    {
        session.OverrideSubscriptionId(subscriptionId);
        return new SessionContext(session);
    }
}
