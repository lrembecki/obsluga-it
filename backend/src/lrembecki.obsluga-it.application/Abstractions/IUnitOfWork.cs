namespace lrembecki.obsluga_it.application.Abstractions;

public interface IUnitOfWork
{
    T GetRepository<T>() where T : IRepository;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
