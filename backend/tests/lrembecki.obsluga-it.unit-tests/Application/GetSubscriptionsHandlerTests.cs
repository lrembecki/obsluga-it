using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.domain.Entities;
using System.Collections;

namespace lrembecki.obsluga_it.unit_tests.Application;

public class GetSubscriptionsHandlerTests
{
    private sealed class FakeUnitOfWork(Hashtable hashset) : IUnitOfWork
    {
        public T GetRepository<T>() where T : IRepository
        {
            var type = typeof(T);
            if (hashset.ContainsKey(type))
            {
                return (T)hashset[type]!;
            }

            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
    }
    private sealed class FakeSubscriptionRepository : ISubscriptionRepository
    {
        private readonly List<SubscriptionVM> _items;
        public FakeSubscriptionRepository(IEnumerable<SubscriptionVM>? items = null)
        {
            _items = items?.ToList() ?? [];
        }
        public Task<List<SubscriptionVM>> GetAllAsync() => Task.FromResult(_items.ToList());
        public Task<Subscription?> GetByIdAsync(Guid id) => Task.FromResult<Subscription?>(null);
    }

    [Fact]
    public async Task HandleAsync_ReturnsAllSubscriptions()
    {
        // Arrange
        var expected = new List<SubscriptionVM>
        {
            new(Guid.NewGuid(), "Sub A"),
            new(Guid.NewGuid(), "Sub B")
        };
        var uow = new FakeUnitOfWork(new Hashtable()
        {
            { typeof(ISubscriptionRepository), new FakeSubscriptionRepository(expected) }
        });
        var handler = new GetSubscriptionsHandler(uow);
        var query = new GetSubscriptions();

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
        var handler = new GetSubscriptionsHandler(uow);
        var query = new GetSubscriptions();

        // Act
        var result = await handler.HandleAsync(query);

        // Assert
        Assert.Empty(result);
    }
}
