using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class UserVMTests
{
    [Fact]
    public void MapFromDomainEntity_MapsEmailAndActiveSubscriptionsOnly()
    {
        var user = UserEntity.Create(Guid.NewGuid(), "user@example.com");

        var sub1 = SubscriptionEntity.Create(Guid.NewGuid(), "Sub1");
        var sub2 = SubscriptionEntity.Create(Guid.NewGuid(), "Sub2");

        var active = SubscriptionUserEntity.Create(Guid.NewGuid(), user, sub1, true);

        var inactive = SubscriptionUserEntity.Create(Guid.NewGuid(), user, sub2, false);
        inactive.Update(false, false);

        user.AddSubscription(active);
        user.AddSubscription(inactive);

        var vm = UserVM.MapFromDomainEntity(user);
        Assert.Equal(user.Id, vm.Id);
        Assert.Equal("user@example.com", vm.Email);
        Assert.Single(vm.Subscriptions);
        Assert.Equal(sub1.Id, vm.Subscriptions[0].Id);
    }
}
