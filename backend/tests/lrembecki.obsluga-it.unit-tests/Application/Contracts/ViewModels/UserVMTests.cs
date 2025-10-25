using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using lrembecki.obsluga_it.unit_tests.Shared;
using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class UserVMTests
{

    [Fact]
    public void MapFromDomainEntity_NoSubscriptions_MapsEmptyList()
    {
        // Arrange
        var user = User.Create(Guid.NewGuid(), new Email("nosubs@example.com"));

        // Act
        var vm = UserVM.MapFromDomainEntity(user);

        // Assert
        Assert.Equal(user.Id, vm.Id);
        Assert.Equal(user.Email.Address, vm.Email);
        Assert.Empty(vm.Subscriptions);
    }

    [Fact]
    public void MapFromDomainEntity_WithSubscriptions_MapsAllFields()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var context = InMemoryApplicationDbContext.Create(subscriptionId: subscriptionId);
        var user = User.Create(Guid.NewGuid(), new Email("alice@example.com"));
        var subscription = Subscription.Create(subscriptionId, "Premium");
        var link = SubscriptionUser.Create(user, true);
        link.User = user;

        context.Set<User>().Add(user);
        context.Set<Subscription>().Add(subscription);
        context.Set<SubscriptionUser>().Add(link);
        context.SaveChanges();

        // Load with navigation fix-up
        var loadedUser = context.Set<User>()
            .Include(u => u.UserSubscriptions)
            .ThenInclude(us => us.Subscription)
            .Single(u => u.Id == user.Id);

        // Act
        var vm = UserVM.MapFromDomainEntity(loadedUser);

        // Assert
        Assert.Equal(user.Id, vm.Id);
        Assert.Equal(user.Email.Address, vm.Email);
        Assert.Single(vm.Subscriptions);
        var subVm = vm.Subscriptions.First();
        Assert.Equal(subscription.Id, subVm.Id);
        Assert.Equal(subscription.Name, subVm.Name);
    }
}
