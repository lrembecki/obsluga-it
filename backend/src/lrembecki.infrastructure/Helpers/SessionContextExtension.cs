using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public static class SessionContextExtension
{
    public static SessionContext CreateSessionContext(this ISessionAccessor session, Guid subscriptionId)
    {
        session.OverrideSubscriptionId(subscriptionId);
        return new SessionContext(session);
    }
}
