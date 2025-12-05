using lrembecki.core.GlobalFilters;
using lrembecki.core.Markers;
using lrembecki.core.Services;
using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace lrembecki.infrastructure.Helpers;

internal class EfRepository<T>(IUnitOfWork uow, ICollection<IGlobalFilter> filters) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = (uow as EfUnitOfWork)!.DbContext.Set<T>();

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

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = (predicate is not null ? _dbSet.Where(predicate) : _dbSet);

        foreach (var filter in filters)
        {
            query = filter.Evaluate(query);
        }

        return query;
    }

    public Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).Select(project).ToListAsync();

    public Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).ToListAsync();

    public async Task<List<T>> AddAsync(List<T> entities)
    {
        _dbSet.AddRange(entities);
        await uow.SaveChangesAsync();

        return entities;
    }

    public async Task<List<T>> DeleteAsync(List<T> entities)
    {
        if (entities.Count > 0)
        {
            _dbSet.RemoveRange(entities);
            await uow.SaveChangesAsync();
        }

        return entities;
    }
}
