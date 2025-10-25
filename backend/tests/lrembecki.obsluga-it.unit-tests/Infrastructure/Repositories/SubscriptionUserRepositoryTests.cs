using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Repositories;
using lrembecki.obsluga_it.unit_tests.Shared;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.unit_tests.Infrastructure.Repositories;

public class SubscriptionUserRepositoryTests
{
    private static (User user, Subscription subscription, SubscriptionUser link) SeedLink(Guid subscriptionId, ApplicationDbContext ctx, string email, string subName)
    {
        var user = User.Create(Guid.NewGuid(), new Email(email));
        var subscription = Subscription.Create(Guid.NewGuid(), subName);
        var link = SubscriptionUser.Create(user, subscription, true);
        link.User = user;
        ctx.Set<User>().Add(user);
        ctx.Set<Subscription>().Add(subscription);
        ctx.Set<SubscriptionUser>().Add(link);
        return (user, subscription, link);
    }

    [Fact]
    public async Task GetByEmailAndSubscriptionId_EmailOnly_ReturnsMatch()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var ctx = InMemoryApplicationDbContext.Create(subscriptionId: subscriptionId);
        var (user, subscription, _) = SeedLink(subscriptionId, ctx, "dave@example.com", "SubA");
        await ctx.SaveChangesAsync();
        var repo = new SubscriptionUserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAndSubscriptionId(user.Email.Address, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.UserId);
        Assert.Equal(subscription.Id, result.SubscriptionId);
        Assert.NotNull(result.User);
        Assert.NotNull(result.Subscription);
    }

    [Fact]
    public async Task GetByEmailAndSubscriptionId_EmailAndSubscription_ReturnsSpecificMatch()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var ctx = InMemoryApplicationDbContext.Create(subscriptionId: subscriptionId);
        var (targetUser, targetSub, _) = SeedLink(subscriptionId, ctx, "erin@example.com", "TargetSub");
        var (_, otherSub, _) = SeedLink(subscriptionId, ctx, "erin@example.com", "OtherSub"); // same email different subscription
        await ctx.SaveChangesAsync();
        var repo = new SubscriptionUserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAndSubscriptionId(targetUser.Email.Address, targetSub.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetSub.Id, result!.SubscriptionId);
        Assert.Equal(targetUser.Id, result.UserId);
        Assert.Equal("TargetSub", result.Subscription.Name);
    }

    [Fact]
    public async Task GetByEmailAndSubscriptionId_WrongSubscription_ReturnsNull()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var ctx = InMemoryApplicationDbContext.Create(subscriptionId: subscriptionId);
        var (user, sub, _) = SeedLink(subscriptionId, ctx, "frank@example.com", "SubX");
        await ctx.SaveChangesAsync();
        var repo = new SubscriptionUserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAndSubscriptionId(user.Email.Address, Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByEmailAndSubscriptionId_EmailNotFound_ReturnsNull()
    {
        // Arrange
        var ctx = InMemoryApplicationDbContext.Create();
        var repo = new SubscriptionUserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAndSubscriptionId("missing@example.com", null);

        // Assert
        Assert.Null(result);
    }
}
