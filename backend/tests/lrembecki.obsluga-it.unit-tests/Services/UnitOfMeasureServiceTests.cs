using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;
using lrembecki.obsluga_it.domain.Enums;
using Xunit;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class UnitOfMeasureServiceTests
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

    private static UnitOfMeasureService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateUnit()
    {
        var service = CreateService();
        var dto = new UnitOfMeasureDto { Name = "Kilogram", ShortName = "kg", ShortCode = "KG", Type = UnitOfMeasureType.Weight };
        var vm = await service.CreateAsync(dto);
        Assert.NotNull(vm);
        Assert.Equal("Kilogram", vm.Name);
        Assert.Equal("kg", vm.ShortName);
        Assert.Equal("KG", vm.ShortCode);
        Assert.Equal(UnitOfMeasureType.Weight, vm.Type);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllUnits()
    {
        var service = CreateService();
        await service.CreateAsync(new UnitOfMeasureDto { Name = "Kilogram", ShortName = "kg", ShortCode = "KG", Type = UnitOfMeasureType.Weight });
        await service.CreateAsync(new UnitOfMeasureDto { Name = "Polish Zloty", ShortName = "PLN", ShortCode = "PLN", Type = UnitOfMeasureType.Currency });
        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, u => u.Name == "Kilogram");
        Assert.Contains(all, u => u.Name == "Polish Zloty");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnUnit()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new UnitOfMeasureDto { Name = "Kilogram", ShortName = "kg", ShortCode = "KG", Type = UnitOfMeasureType.Weight });
        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("Kilogram", fetched.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateUnit()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new UnitOfMeasureDto { Name = "Kilogram", ShortName = "kg", ShortCode = "KG", Type = UnitOfMeasureType.Weight });
        var updated = await service.UpdateAsync(created.Id, new UnitOfMeasureDto { Name = "Gram", ShortName = "g", ShortCode = "G", Type = UnitOfMeasureType.Weight });
        Assert.Equal("Gram", updated.Name);
        Assert.Equal("g", updated.ShortName);
        Assert.Equal("G", updated.ShortCode);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveUnit()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new UnitOfMeasureDto { Name = "Kilogram", ShortName = "kg", ShortCode = "KG", Type = UnitOfMeasureType.Weight });
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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new UnitOfMeasureDto { Name = "X", ShortName = "x", ShortCode = "X", Type = UnitOfMeasureType.Weight }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
