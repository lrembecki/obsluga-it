using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.shared.application.Abstractions;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class TagServiceTests
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

    private static TagService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateTag()
    {
        var service = CreateService();
        var vm = await service.CreateAsync(new TagDto { Name = "TagA" });
        Assert.NotNull(vm);
        Assert.Equal("TagA", vm.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTags()
    {
        var service = CreateService();
        await service.CreateAsync(new TagDto { Name = "A" });
        await service.CreateAsync(new TagDto { Name = "B" });
        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, t => t.Name == "A");
        Assert.Contains(all, t => t.Name == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnTag()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TagDto { Name = "A" });
        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateName()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TagDto { Name = "Old" });
        var updated = await service.UpdateAsync(created.Id, new TagDto { Name = "New" });
        Assert.Equal("New", updated.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveTag()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new TagDto { Name = "A" });
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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new TagDto { Name = "X" }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
