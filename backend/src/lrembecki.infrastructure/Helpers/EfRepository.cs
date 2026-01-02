using lrembecki.core.GlobalFilters;
using lrembecki.core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace lrembecki.infrastructure.Helpers;

internal class EfRepository<T>(IUnitOfWork uow, IServiceProvider serviceProvider, ICollection<IGlobalFilter> filters) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = (uow as EfUnitOfWork)!.DbContext.Set<T>();

    public async Task<T> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        return entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        return entity;
    }

    private IQueryable<T> GetQueryable()
    {
        IQueryable<T> query = _dbSet;

        foreach (var filter in filters)
        {
            query = filter.Evaluate(query);
        }

        return query;
    }

    public IQueryable<T> GetAll<TFilter>(TFilter filter)
    {
        IQueryable<T> query = GetQueryable();
        var filterEvaluator = serviceProvider.GetRequiredService<IFilterEvaluator<T, TFilter>>();
        query = filterEvaluator.Evaluate(query, filter);
        return query;
    }

    public IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null)
    {
        IQueryable<T> query = (predicate is not null ? GetQueryable().Where(predicate) : GetQueryable());

        return query;
    }

    public Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).Select(project).ToListAsync();

    public Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).ToListAsync();

    public async Task<List<T>> AddAsync(List<T> entities)
    {
        _dbSet.AddRange(entities);

        return entities;
    }

    public async Task<List<T>> DeleteAsync(List<T> entities)
    {
        if (entities.Count > 0)
        {
            _dbSet.RemoveRange(entities);
        }

        return entities;
    }
}
