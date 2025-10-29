using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Services;

namespace lrembecki.obsluga_it.unit_tests.Services;

public class ActorServiceTests
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

        public Task<T> AddAsync(T entity)
        {
            _store.Add(entity);
            return Task.FromResult(entity);
        }

        public Task<T> UpdateAsync(T entity)
        {
            // no tracking, entity already mutated
            return Task.FromResult(entity);
        }

        public Task<T> DeleteAsync(T entity)
        {
            _store.Remove(entity);
            return Task.FromResult(entity);
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

    private static ActorService CreateService() => new(new InMemoryUnitOfWork());

    [Fact]
    public async Task CreateAsync_ShouldCreateActorWithContacts()
    {
        // arrange
        var service = CreateService();
        var dto = new ActorDto
        {
            Firstname = "John",
            Lastname = "Doe",
            Contacts = new List<ContactDto>
 {
 new() { Email = "a@example.com", Phone = "123" },
 new() { Email = "b@example.com", Phone = "456" }
 }
        };

        // act
        var vm = await service.CreateAsync(dto);

        // assert
        Assert.NotNull(vm);
        Assert.Equal("John", vm.Firstname);
        Assert.Equal("Doe", vm.Lastname);
        Assert.Equal(2, vm.Contacts.Count);
        Assert.All(vm.Contacts, c => Assert.False(string.IsNullOrWhiteSpace(c.Email)));
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllActors()
    {
        var service = CreateService();
        await service.CreateAsync(new ActorDto { Firstname = "A", Lastname = "1" });
        await service.CreateAsync(new ActorDto { Firstname = "B", Lastname = "2" });

        var all = await service.GetAllAsync();
        Assert.Equal(2, all.Count);
        Assert.Contains(all, a => a.Firstname == "A");
        Assert.Contains(all, a => a.Firstname == "B");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnActor()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new ActorDto { Firstname = "A", Lastname = "1" });

        var fetched = await service.GetByIdAsync(created.Id!.Value);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal("A", fetched.Firstname);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateNames()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new ActorDto { Firstname = "A", Lastname = "1" });

        var updated = await service.UpdateAsync(created.Id!.Value, new ActorDto { Firstname = "New", Lastname = "Name" });

        Assert.Equal("New", updated.Firstname);
        Assert.Equal("Name", updated.Lastname);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveActor()
    {
        var service = CreateService();
        var created = await service.CreateAsync(new ActorDto { Firstname = "A", Lastname = "1" });

        await service.DeleteAsync(created.Id!.Value, CancellationToken.None);

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
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.UpdateAsync(Guid.NewGuid(), new ActorDto { Firstname = "X", Lastname = "Y" }));
    }

    [Fact]
    public async Task DeleteAsync_ShouldThrow_WhenNotFound()
    {
        var service = CreateService();
        await Assert.ThrowsAsync<ArgumentNullException>(() => service.DeleteAsync(Guid.NewGuid(), CancellationToken.None));
    }
}
