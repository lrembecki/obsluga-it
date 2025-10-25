using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using lrembecki.obsluga_it.infrastructure;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class UserVMTests
{
    private static ApplicationDbContext CreateInMemoryDbContext(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
        return new ApplicationDbContext(options);
    }

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
        var context = CreateInMemoryDbContext();
        var user = User.Create(Guid.NewGuid(), new Email("alice@example.com"));
        var subscription = Subscription.Create(Guid.NewGuid(), "Premium");
        var link = SubscriptionUser.Create(user.Id, subscription.Id);
        link.User = user;
        link.Subscription = subscription;

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
