using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.domain.Abstractions;
using System.Linq.Expressions;

namespace lrembecki.obsluga_it.unit_tests.Shared;

internal class FakeRepository<T>(ICollection<T> data = null!) : IRepository<T>
    where T : class, IHasId<Guid>
{
    private readonly List<T> _data = data.ToList();

    public Task<T> AddAsync(T entity)
    {
        _data.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<T> DeleteAsync(T entity)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetAll()
        => _data.AsQueryable();

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
        => (predicate is null ? _data : _data.Where(predicate.Compile())).AsQueryable();

    public Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null)
        => Task.FromResult(GetAll(predicate).Select(project.Compile()).ToList());

    public Task<T> UpdateAsync(T entity)
    {
        _data.Remove(_data.Find(e => e.Id == entity.Id)!);
        _data.Add(entity);

        return Task.FromResult(entity);
    }
}