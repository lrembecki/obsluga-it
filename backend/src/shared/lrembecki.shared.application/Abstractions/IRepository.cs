using System.Linq.Expressions;

namespace lrembecki.shared.application.Abstractions;

public interface IRepository<T>
    where T : class
{
    IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null);
    Task<List<VM>> SelectAsync<VM>(Expression<Func<T, VM>> project, Expression<Func<T, bool>>? predicate = null);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
};
