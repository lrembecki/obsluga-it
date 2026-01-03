using System.Linq.Expressions;

namespace lrembecki.core.Services;

public interface IRepository<T>
    where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
    Task<List<T>> GetAsync(Expression<Func<T, bool>>? predicate = null);
    Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null);
    Task<T> AddAsync(T entity);
    Task<List<T>> AddAsync(List<T> entities);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<List<T>> DeleteAsync(List<T> entities);
    IQueryable<T> GetAll<TFilter>(TFilter filter);
};