using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.ValueObjects;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.unit_tests.Infrastructure.Repositories;

public class SubscriptionUserRepositoryTests
{
    private static ApplicationDbContext CreateInMemoryDbContext(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(dbName ?? Guid.NewGuid().ToString())
        .EnableSensitiveDataLogging()
             .Options;
        return new ApplicationDbContext(options);
    }

    private static (User user, Subscription subscription, SubscriptionUser link) SeedLink(ApplicationDbContext ctx, string email, string subName)
    {
        var user = User.Create(Guid.NewGuid(), new Email(email));
        var subscription = Subscription.Create(Guid.NewGuid(), subName);
        var link = SubscriptionUser.Create(user.Id, subscription.Id);
        link.User = user;
        link.Subscription = subscription;
        ctx.Set<User>().Add(user);
        ctx.Set<Subscription>().Add(subscription);
        ctx.Set<SubscriptionUser>().Add(link);
        return (user, subscription, link);
    }

    [Fact]
    public async Task GetByEmailAndSubscriptionId_EmailOnly_ReturnsMatch()
    {
        // Arrange
        var ctx = CreateInMemoryDbContext();
        var (user, subscription, _) = SeedLink(ctx, "dave@example.com", "SubA");
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
        var ctx = CreateInMemoryDbContext();
        var (targetUser, targetSub, _) = SeedLink(ctx, "erin@example.com", "TargetSub");
        var (_, otherSub, _) = SeedLink(ctx, "erin@example.com", "OtherSub"); // same email different subscription
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
        var ctx = CreateInMemoryDbContext();
        var (user, sub, _) = SeedLink(ctx, "frank@example.com", "SubX");
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
        var ctx = CreateInMemoryDbContext();
        var repo = new SubscriptionUserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAndSubscriptionId("missing@example.com", null);

        // Assert
        Assert.Null(result);
    }
}
