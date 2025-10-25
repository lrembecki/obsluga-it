using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class SubscriptionUserTests
{
    [Fact]
    public void Create_SetsIds_AndDefaults()
    {
        var user = User.Create(Guid.NewGuid(), new Email("test@test.pl"));
        var subscription = Subscription.Create(Guid.NewGuid(), "test");
        var link = SubscriptionUser.Create(user, subscription, false);

        Assert.Equal(user.Id, link.UserId);
        Assert.True(link.IsActive);
        Assert.False(link.IsDefault);
    }
}
