using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Extensions;
using lrembecki.obsluga_it.domain.Abstractions;
using System.Linq.Expressions;

namespace lrembecki.obsluga_it.application.Extensions;

public static class RepositoryExtensions
{
    public static Task<T?> GetByIdAsync<T>(this IRepository<T> repository, Guid id)
        where T : class, IHasId<Guid>
        => Task.Run(() => repository.FirstOrDefaultAsync(e => e.Id == id));

    public static Task<T?> FirstOrDefaultAsync<T>(this IRepository<T> repository, Expression<Func<T, bool>> predicate)
        where T : class
        => Task.Run(() => repository.GetAll().FirstOrDefault(predicate));
}
