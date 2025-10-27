using lrembecki.obsluga_it.application.Abstractions.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace lrembecki.obsluga_it.infrastructure.Persistence;

internal class EfUnitOfWork(IServiceProvider serviceProvider, ApplicationDbContext dbContext) : IUnitOfWork
{
    public T GetRepository<T>() where T : IRepository
        => serviceProvider.GetRequiredService<T>();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => dbContext.SaveChangesAsync(cancellationToken);
}
