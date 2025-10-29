using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Contracts.Queries;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Handlers;

internal sealed class LoyalityProgramHandler(IUnitOfWork uow) : IRequestHandler<LoyalityProgramsGetQuery, List<LoyalityProgramVM>>
{
    public Task<List<LoyalityProgramVM>> HandleAsync(LoyalityProgramsGetQuery request, CancellationToken cancellationToken = default)
    => Task.Run(() => uow.GetRepository<LoyalityProgramEntity>()
        .GetAll()
        .Select(LoyalityProgramVM.MapFromDomainEntity)
        .ToList(), cancellationToken);
}
