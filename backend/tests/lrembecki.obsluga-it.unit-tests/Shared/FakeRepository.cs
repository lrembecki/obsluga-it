using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Specifications;
using lrembecki.obsluga_it.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

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

    public IQueryable<T> GetAll()
        => _data.AsQueryable();

    public IQueryable<T> GetAll(Specification<T> specification = null!)
        => _data.Where((specification ?? Specification<T>.All).Predicate.Compile()).AsQueryable();

    public Task<List<VM>> SelectAsync<VM>(Specification<T, VM> specification)
        => GetAll(specification).Select(specification.Project).ToListAsync();

    public Task<T> UpdateAsync(T entity)
    {
        _data.Remove(_data.Find(e => e.Id == entity.Id)!);
        _data.Add(entity);

        return Task.FromResult(entity);
    }
}