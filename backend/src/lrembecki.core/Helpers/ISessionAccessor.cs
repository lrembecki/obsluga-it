namespace lrembecki.core.Helpers
{
    public interface ISessionAccessor
    {
        bool IsAuthenticated { get; }
        string? Email { get; }
        Guid? SubscriptionId { get; }
        Guid? UserId { get; }
        void OverrideSubscriptionId(Guid? subscriptionId);
    }
}
