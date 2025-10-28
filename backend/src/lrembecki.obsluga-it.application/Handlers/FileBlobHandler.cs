using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class FileBlobHandler(IUnitOfWork uow) : IRequestHandler<FileBlobsGetQuery, List<FileBlobVM>>
{
    public Task<List<FileBlobVM>> HandleAsync(FileBlobsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<IRepository<FileBlobEntity>>()
    .GetAll()
    .Select(FileBlobVM.MapFromDomainEntity)
    .ToList(), cancellationToken);
}
