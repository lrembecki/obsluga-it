using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.obsluga_it.unit_tests.Shared;
using System.Collections;

namespace lrembecki.obsluga_it.unit_tests.Application.Queries;

public class GetSubscriptionsHandlerTests
{
    private sealed class FakeSubscriptionRepository(List<SubscriptionEntity>? items = null) : FakeRepository<SubscriptionEntity>(items ?? []), ISubscriptionRepository
    {
        public Task<List<SubscriptionVM>> GetAllSubscriptionVMAsync(CancellationToken cancellationToken = default)
            => Task.FromResult(GetAll().Select(SubscriptionVM.MapFromDomainEntity).ToList());
    }

    [Fact]
    public async Task HandleAsync_ReturnsAllSubscriptions()
    {
        // Arrange
        var expected = new List<SubscriptionEntity>
        {
            SubscriptionEntity.Create(Guid.NewGuid(), "Sub A"),
            SubscriptionEntity.Create(Guid.NewGuid(), "Sub B")
        };

        var uow = new FakeUnitOfWork(new Hashtable()
        {
            { typeof(ISubscriptionRepository), new FakeSubscriptionRepository(expected) }
        });

        var handler = new SubscriptionsGetQueryHandler(uow);
        var query = new SubscriptionsGetQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.Equal(expected.Count, result.Count);
        Assert.True(expected.All(e => result.Any(r => r.Id == e.Id && r.Name == e.Name)));
    }

    [Fact]
    public async Task HandleAsync_EmptyList_ReturnsEmpty()
    {
        // Arrange
        var uow = new FakeUnitOfWork(new Hashtable()
        {
            { typeof(ISubscriptionRepository), new FakeSubscriptionRepository() }
        });
        var handler = new SubscriptionsGetQueryHandler(uow);
        var query = new SubscriptionsGetQuery();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.Empty(result);
    }
}
