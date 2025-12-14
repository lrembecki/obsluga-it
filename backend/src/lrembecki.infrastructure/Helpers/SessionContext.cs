using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

public class SessionContext(ISessionAccessor session) : IDisposable
{
    public void Dispose()
    {
        session.OverrideSubscriptionId(null);
    }
}