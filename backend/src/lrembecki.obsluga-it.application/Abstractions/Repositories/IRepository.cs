namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public interface IRepository;
public interface IRepository<T> : IRepository
    where T : class
{
    IQueryable<T> GetAll();
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(T entity);
};
