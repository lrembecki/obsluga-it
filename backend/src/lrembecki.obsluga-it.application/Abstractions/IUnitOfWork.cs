namespace lrembecki.obsluga_it.application.Abstractions;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
