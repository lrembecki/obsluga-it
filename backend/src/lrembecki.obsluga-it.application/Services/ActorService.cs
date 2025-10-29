using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;
internal sealed class ActorService(IUnitOfWork uow) : IActorService
{
    private readonly IRepository<ActorEntity> _actors = uow.GetRepository<ActorEntity>();

    public Task<List<ActorVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => _actors.SelectAsync(e => ActorVM.MapFromDomainEntity(e));

    public async Task<ActorVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        => ActorVM.MapFromDomainEntity(await RequireById(id, cancellationToken));

    public async Task<ActorVM> CreateAsync(ActorDto model, CancellationToken cancellationToken = default)
    {
        var actor = ActorEntity.Create(Guid.NewGuid(), model.Firstname, model.Lastname);
        await _actors.AddAsync(actor);

        model.Contacts
            .Select(contact => ContactEntity.Create(Guid.NewGuid(), contact.Email, contact.Phone))
            .ToList()
            .ForEach(actor.AddContact);

        return await GetByIdAsync(actor.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var actor = await RequireById(id, cancellationToken);
        await _actors.DeleteAsync(actor);
    }

    public async Task<ActorVM> UpdateAsync(Guid id, ActorDto model, CancellationToken cancellationToken = default)
    {
        var actor = await RequireById(id, cancellationToken);

        actor.Update(model.Firstname, model.Lastname);

        await _actors.UpdateAsync(actor);

        return await GetByIdAsync(actor.Id, cancellationToken);
    }

    private async Task<ActorEntity> RequireById(Guid id, CancellationToken cancellationToken)
        => await _actors.FindByIdAsync(id, cancellationToken)
            ?? throw new ArgumentNullException(nameof(id), "Actor not found");
}
