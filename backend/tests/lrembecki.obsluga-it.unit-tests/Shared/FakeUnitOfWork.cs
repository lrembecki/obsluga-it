using lrembecki.obsluga_it.application.Abstractions.Repositories;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using System.Collections;

namespace lrembecki.obsluga_it.unit_tests.Shared;

internal sealed class FakeUnitOfWork(Hashtable hashset) : IUnitOfWork
{
    public IRepository<T> GetRepository<T>() where T : class
    {
        var type = typeof(T);
        if (hashset.ContainsKey(type))
        {
            return (IRepository<T>)hashset[type]!;
        }

        throw new NotImplementedException();
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
}
