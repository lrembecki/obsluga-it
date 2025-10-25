using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Queries;
using lrembecki.obsluga_it.domain.Entities;
using System.Collections;

namespace lrembecki.obsluga_it.unit_tests.Application.Queries;

public class GetSubscriptionsHandlerTests
{
    private sealed class FakeSubscriptionRepository(IEnumerable<SubscriptionVM>? items = null) : ISubscriptionRepository
    {
        private readonly List<SubscriptionVM> _items = items?.ToList() ?? [];

        public Task<SubscriptionVM> AddAsync(Subscription subscription)
        {
            var subscriptionVM = SubscriptionVM.MapFromDomainEntity(subscription);
            
            _items.Add(subscriptionVM);

            return Task.FromResult(subscriptionVM);
        }

        public Task<List<SubscriptionVM>> GetAllAsync() => Task.FromResult(_items.ToList());
        public Task<Subscription?> GetByIdAsync(Guid id) => Task.FromResult<Subscription?>(null);

        public Task<SubscriptionVM> UpdateAsync(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        Task<Subscription> ISubscriptionRepository.AddAsync(Subscription subscription)
        {
            _items.Add(SubscriptionVM.MapFromDomainEntity(subscription));
            return Task.FromResult(subscription);
        }

        Task<Subscription> ISubscriptionRepository.UpdateAsync(Subscription subscription)
        {
            _items.Remove(_items.Find(e => e.Id == subscription.Id)!);
            _items.Add(SubscriptionVM.MapFromDomainEntity(subscription));
            return Task.FromResult(subscription);
        }
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
