using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class SubscriptionTests
{
    [Fact]
    public void Create_SetsProperties_AndAddsDomainEvent()
    {
        var id = Guid.NewGuid();
        var sub = SubscriptionEntity.Create(id, "Alpha");

        Assert.Equal(id, sub.Id);
        Assert.Equal("Alpha", sub.Name);
        Assert.Empty(sub.UserSubscriptions);
        Assert.Single(sub.DomainEvents); // from BaseEntity
        var evt = sub.DomainEvents.First();
        Assert.Equal(id, ((dynamic)evt).SubscriptionId);
    }
}
