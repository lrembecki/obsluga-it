using lrembecki.obsluga_it.infrastructure;
using lrembecki.obsluga_it.infrastructure.Repositories;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.unit_tests.Shared;

namespace lrembecki.obsluga_it.unit_tests.Infrastructure.Repositories;

public class SubscriptionRepositoryTests
{

    [Fact]
    public async Task GetAllAsync_ReturnsMappedViewModels()
    {
        // Arrange
        var context = InMemoryApplicationDbContext.Create();
        var subs = new[]
        {
            SubscriptionEntity.Create(Guid.NewGuid(), "Alpha"),
            SubscriptionEntity.Create(Guid.NewGuid(), "Beta")
        };
        context.Set<SubscriptionEntity>().AddRange(subs);
        await context.SaveChangesAsync();
        var repo = new SubscriptionRepository(context);

        // Act
        var result = await repo.GetAllSubscriptionVMAsync();

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
        var context = InMemoryApplicationDbContext.Create();
        var repo = new SubscriptionRepository(context);

        // Act
        var result = await repo.GetAllSubscriptionVMAsync();

        // Assert
        Assert.Empty(result);
    }
}
