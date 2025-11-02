using lrembecki.shared.application.Abstractions;
using lrembecki.trotamundos.application.Dtos;
using lrembecki.trotamundos.application.Services;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class TripServiceTests
{
    private class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _store = [];
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
        public Task<T> UpdateAsync(T entity) => Task.FromResult(entity); // entity already mutated
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

    private static TripService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateTrip()
    {
        var service = CreateService();
        var dto = new TripDto { Title = "Title", Subtitle = "Sub", Description = "Desc" };
        var vm = await service.CreateAsync(dto);
        Assert.NotNull(vm);
        Assert.Equal("Title", vm.Title);
        Assert.Equal("Sub", vm.Subtitle);
        Assert.Equal("Desc", vm.Description);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTrips()
    {
        var service = CreateService();
        await service.CreateAsync(new TripDto { Title = "A", Subtitle = "S1", Description = "D1" });
        await service.CreateAsync(new TripDto { Title = "B", Subtitle = "S2", Description = "D2" });
        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, t => t.Title == "A");
        Assert.Contains(all, t => t.Title == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTrip()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TripDto { Title = "A", Subtitle = "S", Description = "D" });
        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateTrip()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TripDto { Title = "Old", Subtitle = "OldS", Description = "OldD" });
        var updated = await service.UpdateAsync(created.Id, new TripDto { Title = "New", Subtitle = "NewS", Description = "NewD" });
        Assert.Equal("New", updated.Title);
        Assert.Equal("NewS", updated.Subtitle);
        Assert.Equal("NewD", updated.Description);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTrip()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TripDto { Title = "A", Subtitle = "S", Description = "D" });
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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new TripDto { Title = "X", Subtitle = "Y", Description = "Z" }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
