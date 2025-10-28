using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.unit_tests.Application.Contracts.ViewModels;

public class AccountVMTests
{
    [Fact]
    public void Constructor_SetsAllProperties()
    {
        var user = new UserVM(Guid.NewGuid(), "user@example.com", new List<SubscriptionVM>());
        var subscription = new SubscriptionVM(Guid.NewGuid(), "Sub");
        var permissions = new List<string> { "perm1", "perm2" };
        var token = "token123";
        var created = DateTime.UtcNow;
        var expires = created.AddHours(1);
        var vm = new AccountVM(user, subscription, permissions, token, created, expires, true);

        Assert.Equal(user, vm.User);
        Assert.Equal(subscription, vm.Subscription);
        Assert.Equal(permissions, vm.Permissions);
        Assert.Equal(token, vm.Token);
        Assert.Equal(created, vm.Created);
        Assert.Equal(expires, vm.Expires);
        Assert.True(vm.IsAuthenticated);
    }
}
