using lrembecki.core.Helpers;
using lrembecki.core.Markers;
using lrembecki.core.Services;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace lrembecki.infrastructure.Helpers;

internal class EfRepository<T>(IUnitOfWork uow, ISessionAccessor sessionAccessor) : IRepository<T>
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

        if (typeof(T).IsAssignableTo(typeof(IHasSubscriptionId)))
        {
            var subscriptionId = sessionAccessor.SubscriptionId;

            var subscriptionPredicate = (Expression<Func<T, bool>>)GetType()
                .GetMethod(nameof(SubscriptionIdPredicate))!
                .MakeGenericMethod(typeof(T)).Invoke(this, [])!;

            query = query.Where(subscriptionPredicate);
        }

        return query;
    }

    public Expression<Func<Y, bool>> SubscriptionIdPredicate<Y>()
        where Y : IHasSubscriptionId
        => item => item.SubscriptionId == sessionAccessor.SubscriptionId;

    public Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).Select(project).ToListAsync();

    public Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicate = null)
        => GetAll(predicate).ToListAsync();
}
