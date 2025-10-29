using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using Xunit;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class SubscriptionServiceTests
{
    private class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _store = new();
        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
        {
            var q = _store.AsQueryable();
            if (predicate != null) q = q.Where(predicate);
            return q;
        }
        public Task<List<VM>> SelectAsync<VM>(System.Linq.Expressions.Expression<Func<T, VM>> project, System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
        {
            var q = GetAll(predicate);
            return Task.FromResult(q.Select(project.Compile()).ToList());
        }
        public Task<T> AddAsync(T entity) { _store.Add(entity); return Task.FromResult(entity); }
        public Task<T> UpdateAsync(T entity) => Task.FromResult(entity);
        public Task<T> DeleteAsync(T entity) { _store.Remove(entity); return Task.FromResult(entity); }
    }
    private class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repos = new();
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repos.TryGetValue(typeof(T), out var repo)) return (IRepository<T>)repo;
            var newRepo = new InMemoryRepository<T>();
            _repos[typeof(T)] = newRepo;
            return newRepo;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
    }

    private static SubscriptionService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateSubscription()
    {
        var service = CreateService();
        var vm = await service.CreateAsync(new SubscriptionDto { Name = "SubA" });
        Assert.NotNull(vm);
        Assert.Equal("SubA", vm.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllSubscriptions()
    {
        var service = CreateService();
        await service.CreateAsync(new SubscriptionDto { Name = "A" });
        await service.CreateAsync(new SubscriptionDto { Name = "B" });
        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, s => s.Name == "A");
        Assert.Contains(all, s => s.Name == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnSubscription()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new SubscriptionDto { Name = "A" });
        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateName()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new SubscriptionDto { Name = "Old" });
        var updated = await service.UpdateAsync(created.Id, new SubscriptionDto { Name = "New" });
        Assert.Equal("New", updated.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveSubscription()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new SubscriptionDto { Name = "A" });
        await service.DeleteAsync(created.Id, CancellationToken.None);
        var all = await service.GetAllAsync();
        Assert.Empty(all);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.GetByIdAsync(Guid.NewGuid()));
    }

    [Fact]
    public async Task UpdateAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new SubscriptionDto { Name = "X" }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
