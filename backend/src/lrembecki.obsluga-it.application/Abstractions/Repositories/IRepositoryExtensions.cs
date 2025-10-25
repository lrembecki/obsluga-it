using lrembecki.obsluga_it.domain.Abstractions;

namespace lrembecki.obsluga_it.application.Abstractions.Repositories;

public static class IRepositoryExtensions
{
    public static Task<T?> FindByIdAsync<T>(
        this IRepository<T> repository,
        Guid id, CancellationToken cancellationToken)
        where T : class, IHasId<Guid>
        => Task.Run(() => repository.GetAll().SingleOrDefault(e => e.Id == id), cancellationToken);
}