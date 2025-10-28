using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class UserHandler(IUnitOfWork uow) : IRequestHandler<UsersGetQuery, List<UserVM>>
{
    public Task<List<UserVM>> HandleAsync(UsersGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<IRepository<UserEntity>>()
    .GetAll()
    .Select(UserVM.MapFromDomainEntity)
    .ToList(), cancellationToken);
}
