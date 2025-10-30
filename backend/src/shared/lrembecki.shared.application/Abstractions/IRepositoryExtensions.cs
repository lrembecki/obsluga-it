using lrembecki.shared.domain.Abstractions;

namespace lrembecki.shared.application.Abstractions;

public static class IRepositoryExtensions
{
    public static Task<T?> FindByIdAsync<T>(
        this IRepository<T> repository,
        Guid id, CancellationToken cancellationToken)
        where T : class, IHasId<Guid>
        => Task.Run(() => repository.GetAll().SingleOrDefault(e => e.Id == id), cancellationToken);
}