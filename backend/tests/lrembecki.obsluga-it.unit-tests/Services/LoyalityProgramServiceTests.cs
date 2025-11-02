using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Dtos;
using lrembecki.shared.application.Services;
using lrembecki.trotamundos.application.Dtos;
using lrembecki.trotamundos.application.Services;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class LoyalityProgramServiceTests
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
        public Task<T> UpdateAsync(T entity) => Task.FromResult(entity);
        public Task<T> DeleteAsync(T entity) { _store.Remove(entity); return Task.FromResult(entity); }
    }
    private class InMemoryBlobService : IBlobService
    {
        public Task RemoveBlobAsync(string blobPath, string container, CancellationToken cancellationToken) => Task.CompletedTask;
        public Task<T> SyncBlobDataAsync<T>(T model, string container, CancellationToken cancellationToken) where T : BlobDto
        {
            if (model.Id is null) model.Id = Guid.NewGuid();
            if (string.IsNullOrWhiteSpace(model.Filename)) model.Filename = $"{model.Id}.png";
            model.BlobPath ??= $"{container}/{model.Filename}";
            model.BlobUrl ??= $"https://example.com/{model.BlobPath}";
            model.Size ??= 123;
            return Task.FromResult(model);
        }
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
    private static LoyalityProgramService CreateService() => new(new InMemoryUnitOfWork(), new InMemoryBlobService());

    [Fact]
    public async Task CreateAsync_ShouldCreateProgramWithImage()
    {
        var service = CreateService();
        var dto = new LoyalityProgramDto { Name = "Name", Title = "Title", Description = "Desc", Image = new ImageBlobDto() };

        var vm = await service.CreateAsync(dto);

        Assert.NotNull(vm);
        Assert.Equal("Name", vm.Name);
        Assert.Equal("Title", vm.Title);
        Assert.Equal("Desc", vm.Description);
        Assert.NotNull(vm.Image);
        Assert.False(string.IsNullOrWhiteSpace(vm.Image.BlobUrl));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllPrograms()
    {
        var service = CreateService();
        await service.CreateAsync(new LoyalityProgramDto { Name = "A", Title = "T1", Description = "D1", Image = new ImageBlobDto() });
        await service.CreateAsync(new LoyalityProgramDto { Name = "B", Title = "T2", Description = "D2", Image = new ImageBlobDto() });

        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, p => p.Name == "A");
        Assert.Contains(all, p => p.Name == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnProgram()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new LoyalityProgramDto { Name = "A", Title = "T", Description = "D", Image = new ImageBlobDto() });
        var fetched = await service.GetByIdAsync(created.Id);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProgramAndImageMetadata()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new LoyalityProgramDto { Name = "A", Title = "T", Description = "D", Image = new ImageBlobDto() });

        var updated = await service.UpdateAsync(created.Id, new LoyalityProgramDto { Name = "New", Title = "NewT", Description = "NewD", Image = new ImageBlobDto { Id = created.Image.Id, DisplayName = "Disp" } });

        Assert.Equal("New", updated.Name);
        Assert.Equal("NewT", updated.Title);
        Assert.Equal("NewD", updated.Description);
        Assert.Equal(created.Image.Id, updated.Image.Id);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProgram()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new LoyalityProgramDto { Name = "A", Title = "T", Description = "D", Image = new ImageBlobDto() });
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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new LoyalityProgramDto { Name = "X", Title = "Y", Description = "Z", Image = new ImageBlobDto() }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
