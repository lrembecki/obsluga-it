using lrembecki.core.Markers;

namespace lrembecki.core.Services
{
    public static class IRepositoryExtensions
    {
        public static Task<T?> FindByIdAsync<T>(
            this IRepository<T> repository,
            Guid id, 
            CancellationToken cancellationToken
        ) where T : class, IHasId<Guid>
            => Task.Run(() => repository.GetAll().SingleOrDefault(e => e.Id == id), cancellationToken);

        public static Task<T> RequireByIdAsync<T>(
            this IRepository<T> repository, 
            Guid id, 
            CancellationToken cancellationToken
        ) where T : class, IHasId<Guid>
            => Task.Run(() =>
            {
                var entity = repository.GetAll().SingleOrDefault(e => e.Id == id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with Id {id} was not found.");
                return entity;
            }, cancellationToken);
    }
}