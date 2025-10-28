using lrembecki.obsluga_it.application.Abstractions.Factories;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Specifications;
using lrembecki.obsluga_it.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Persistence;

internal class EfRepository<T>(IUnitOfWork uow, ISessionAccessor session) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = (uow as EfUnitOfWork)!.DbContext.Set<T>();

    public virtual IQueryable<T> GetAll(Specification<T> specification = null!)
    {
        specification ??= Specification<T>.All;

        specification = specification.ReflectWithSubscriptionId(session.SubscriptionId);

        return _dbSet.Where(specification?.Predicate ?? Specification<T>.All.Predicate).AsNoTracking(); ;
    }

    public Task<List<VM>> SelectAsync<VM>(Specification<T, VM> specification)
        => GetAll(specification).Select(specification.Project).ToListAsync();

    public async Task<T> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await uow.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await uow.SaveChangesAsync();
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await uow.SaveChangesAsync();
        return entity;
    }
}
