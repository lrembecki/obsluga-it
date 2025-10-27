using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace lrembecki.obsluga_it.infrastructure.Extensions;

public static class RepositoryExtensions
{
    public static Task<T?> GetByIdAsync<T>(this IRepository<T> repository, Guid id)
        where T : class, IHasId<Guid>
        => Task.Run(() => repository.GetAll().FirstOrDefaultAsync(e => e.Id == id));
}
