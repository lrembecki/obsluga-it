using lrembecki.obsluga_it.application.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Repositories;

internal class EfRepository<T>(ApplicationDbContext dbContext): IRepository<T>
    where T : class
{
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public IQueryable<T> GetAll() => _dbSet.AsNoTracking();

    public async Task<T> AddAsync(T entity)
    {
        _dbSet.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }
}
