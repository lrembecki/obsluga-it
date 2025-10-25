using lrembecki.obsluga_it.application.Abstractions.Repositories;
using System.Collections;

namespace lrembecki.obsluga_it.unit_tests.Application;

internal sealed class FakeUnitOfWork(Hashtable hashset) : IUnitOfWork
{
    public T GetRepository<T>() where T : IRepository
    {
        var type = typeof(T);
        if (hashset.ContainsKey(type))
        {
            return (T)hashset[type]!;
        }

        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
}
