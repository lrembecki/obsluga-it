using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class FileGroupHandler(IUnitOfWork uow) : IRequestHandler<FileGroupsGetQuery, List<FileGroupVM>>
{
    public Task<List<FileGroupVM>> HandleAsync(FileGroupsGetQuery request, CancellationToken cancellationToken = default)
        => uow.GetRepository<IFileGroupRepository>().GetAllAsync(cancellationToken);
}
