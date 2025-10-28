using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Domain.Entities;

public class SubscriptionUserEntityTests
{
    [Fact]
    public void Create_SetsProperties()
    {
        var id = Guid.NewGuid();
        var user = UserEntity.Create(Guid.NewGuid(), "user@example.com");
        var subscription = SubscriptionEntity.Create(Guid.NewGuid(), "Sub");
        var entity = SubscriptionUserEntity.Create(id, user, subscription, true);
        Assert.Equal(id, entity.Id);
        Assert.Equal(user.Id, entity.UserId);
        Assert.Equal(subscription.Id, entity.SubscriptionId);
        Assert.True(entity.IsActive);
        Assert.True(entity.IsDefault);
    }

    [Fact]
    public void Update_ChangesFlags()
    {
        var entity = SubscriptionUserEntity.Create(Guid.NewGuid(), UserEntity.Create(Guid.NewGuid(), "u@e.com"), SubscriptionEntity.Create(Guid.NewGuid(), "Sub"), false);
        entity.Update(true, false);
        Assert.True(entity.IsDefault);
        Assert.False(entity.IsActive);
    }
}
