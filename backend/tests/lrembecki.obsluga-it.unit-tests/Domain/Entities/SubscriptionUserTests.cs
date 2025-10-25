using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class SubscriptionUserTests
{
    [Fact]
    public void Create_SetsIds_AndDefaults()
    {
        var userId = Guid.NewGuid();
        var subId = Guid.NewGuid();
        var link = SubscriptionUser.Create(userId, subId);

        Assert.Equal(userId, link.UserId);
        Assert.Equal(subId, link.SubscriptionId);
        Assert.True(link.IsActive);
        Assert.False(link.IsDefault);
    }
}
