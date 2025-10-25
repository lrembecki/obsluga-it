using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.domain.Entities.SubscriptionEntities;
using lrembecki.obsluga_it.domain.ValueObjects;
using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Repositories;
using lrembecki.obsluga_it.unit_tests.Shared;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.unit_tests.Infrastructure.Repositories;

public class UserRepositoryTests
{
    [Fact]
    public async Task AddAsync_PersistsUser()
    {
        // Arrange
        var ctx = InMemoryApplicationDbContext.Create();
        var repo = new UserRepository(ctx);
        var user = User.Create(Guid.NewGuid(), new Email("alice@example.com"));

        // Act
        await repo.AddAsync(user);

        // Assert
        var stored = await ctx.Set<User>().FirstOrDefaultAsync(e => e.Id == user.Id);
        Assert.NotNull(stored);
        Assert.Equal(user.Email.Address, stored!.Email.Address);
    }

    [Fact]
    public async Task GetByEmailAsync_ReturnsUserWithSubscriptions()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var ctx = InMemoryApplicationDbContext.Create(subscriptionId: subscriptionId);
        var user = User.Create(Guid.NewGuid(), new Email("bob@example.com"));
        var subscription = Subscription.Create(subscriptionId, "Main");
        var link = SubscriptionUser.Create(user, true);
        link.User = user;

        ctx.Set<User>().Add(user);
        ctx.Set<Subscription>().Add(subscription);
        ctx.Set<SubscriptionUser>().Add(link);
        await ctx.SaveChangesAsync();
        var repo = new UserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAsync(user.Email.Address);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result!.Id);
        Assert.Single(result.UserSubscriptions);
        Assert.Equal(subscription.Id, result.UserSubscriptions.First().SubscriptionId);
    }

    [Fact]
    public async Task GetByEmailAsync_NotFound_ReturnsNull()
    {
        // Arrange
        var ctx = InMemoryApplicationDbContext.Create();
        var repo = new UserRepository(ctx);

        // Act
        var result = await repo.GetByEmailAsync("missing@example.com");

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsUser()
    {
        // Arrange
        var ctx = InMemoryApplicationDbContext.Create();
        var user = User.Create(Guid.NewGuid(), new Email("carol@example.com"));
        ctx.Set<User>().Add(user);
        await ctx.SaveChangesAsync();
        var repo = new UserRepository(ctx);

        // Act
        var result = await repo.GetByIdAsync(user.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Email.Address, result!.Email.Address);
    }

    [Fact]
    public async Task GetByIdAsync_NotFound_ReturnsNull()
    {
        // Arrange
        var ctx = InMemoryApplicationDbContext.Create();
        var repo = new UserRepository(ctx);

        // Act
        var result = await repo.GetByIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(result);
    }
}
