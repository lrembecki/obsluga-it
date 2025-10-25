using lrembecki.obsluga_it.application.Abstractions.Factories;

namespace lrembecki.obsluga_it.unit_tests.Shared;

internal sealed class FakeSessionAccessor(Guid subscriptionId, Guid userId) : ISessionAccessor
{
    public bool IsAuthenticated => true;

    public string? Email => "test@test.pl";

    public Guid? SubscriptionId => subscriptionId;

    public Guid? UserId => userId;

    public static FakeSessionAccessor Dummy(Guid? subscriptionId = null, Guid? userId = null) => new FakeSessionAccessor(subscriptionId ?? Guid.NewGuid(), userId ?? Guid.NewGuid());
}