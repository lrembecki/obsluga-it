using lrembecki.obsluga_it.application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Persistence;

internal class EfRepository<T>(IUnitOfWork uow) : IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = (uow as EfUnitOfWork)!.DbContext.Set<T>();

    public virtual IQueryable<T> GetAll() => _dbSet.AsNoTracking();

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
}
