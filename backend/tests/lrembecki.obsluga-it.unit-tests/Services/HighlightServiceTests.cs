using lrembecki.shared.application.Abstractions;
using lrembecki.trotamundos.application.Dtos;
using lrembecki.trotamundos.application.Services;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class HighlightServiceTests
{
    private class InMemoryRepository<T> : IRepository<T> where T : class
    {
        private readonly List<T> _store = new();
        public IQueryable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
        {
            var query = _store.AsQueryable();
            if (predicate != null) query = query.Where(predicate);
            return query;
        }
        public Task<List<VM>> SelectAsync<VM>(System.Linq.Expressions.Expression<Func<T, VM>> project, System.Linq.Expressions.Expression<Func<T, bool>>? predicate = null)
        {
            var query = GetAll(predicate);
            return Task.FromResult(query.Select(project.Compile()).ToList());
        }
        public Task<T> AddAsync(T entity) { _store.Add(entity); return Task.FromResult(entity); }
        public Task<T> UpdateAsync(T entity) => Task.FromResult(entity); // entity already mutated
        public Task<T> DeleteAsync(T entity) { _store.Remove(entity); return Task.FromResult(entity); }
    }
    private class InMemoryUnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        public IRepository<T> GetRepository<T>() where T : class
        {
            if (_repositories.TryGetValue(typeof(T), out var repo)) return (IRepository<T>)repo;
            var newRepo = new InMemoryRepository<T>();
            _repositories[typeof(T)] = newRepo;
            return newRepo;
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
    }

    private static HighlightService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateHighlight()
    {
        var service = CreateService();
        var dto = new HighlightDto { Title = "T", Icon = "I" };

        var vm = await service.CreateAsync(dto);

        Assert.NotNull(vm);
        Assert.Equal("T", vm.Title);
        Assert.Equal("I", vm.Icon);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllHighlights()
    {
        var service = CreateService();
        await service.CreateAsync(new HighlightDto { Title = "A", Icon = "1" });
        await service.CreateAsync(new HighlightDto { Title = "B", Icon = "2" });

        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, h => h.Title == "A");
        Assert.Contains(all, h => h.Title == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnHighlight()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new HighlightDto { Title = "A", Icon = "1" });

        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Title);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateHighlight()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new HighlightDto { Title = "Old", Icon = "OldI" });

        var updated = await service.UpdateAsync(created.Id, new HighlightDto { Title = "New", Icon = "NewI" });

        Assert.Equal("New", updated.Title);
        Assert.Equal("NewI", updated.Icon);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveHighlight()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new HighlightDto { Title = "A", Icon = "1" });

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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new HighlightDto { Title = "X", Icon = "Y" }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
