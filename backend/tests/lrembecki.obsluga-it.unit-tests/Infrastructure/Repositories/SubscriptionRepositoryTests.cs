using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.unit_tests.Infrastructure.Repositories;

public class SubscriptionRepositoryTests
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
    public async Task GetAllAsync_ReturnsMappedViewModels()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        var subs = new[]
        {
            Subscription.Create(Guid.NewGuid(), "Alpha"),
            Subscription.Create(Guid.NewGuid(), "Beta")
        };
        context.Set<Subscription>().AddRange(subs);
        await context.SaveChangesAsync();
        var repo = new SubscriptionRepository(context);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.Equal(subs.Length, result.Count);
        foreach (var vm in result)
        {
            var match = subs.Single(s => s.Id == vm.Id);
            Assert.Equal(match.Name, vm.Name);
        }
    }

    [Fact]
    public async Task GetAllAsync_Empty_ReturnsEmptyList()
    {
        // Arrange
        var context = CreateInMemoryDbContext();
        var repo = new SubscriptionRepository(context);

        // Act
        var result = await repo.GetAllAsync();

        // Assert
        Assert.Empty(result);
    }
}
