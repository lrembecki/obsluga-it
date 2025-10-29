using lrembecki.obsluga_it.application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lrembecki.obsluga_it.infrastructure.Persistence;

internal class EfRepository<T>(IUnitOfWork uow) : IRepository<T>
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
        => predicate is not null ? _dbSet.Where(predicate) : _dbSet;

    public Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).Select(project).ToListAsync();
}
