using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class ContactHandler(IUnitOfWork uow) : IRequestHandler<ContactsGetQuery, List<ContactVM>>
{
    public Task<List<ContactVM>> HandleAsync(ContactsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<IRepository<ContactEntity>>()
    .GetAll()
    .Select(ContactVM.MapFromDomainEntity)
    .ToList(), cancellationToken);
}
