using lrembecki.obsluga_it.application.Abstractions.Repositories;
using lrembecki.obsluga_it.application.Abstractions.Services;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.application.Handlers;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;

public interface IActorService : ICrudService<ActorDto, ActorVM>;

internal sealed class ActorService(IUnitOfWork uow) : IActorService
{
    private readonly IRepository<ActorEntity> _actors = uow.GetRepository<IRepository<ActorEntity>>();

    public Task<List<ActorVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => uow.GetRepository<IRepository<ActorEntity>>().SelectAsync(e => ActorVM.MapFromDomainEntity(e));

    public async Task<ActorVM> GetByIdAsync(Guid actorId, CancellationToken cancellationToken = default)
        => ActorVM.MapFromDomainEntity(await RequireById(actorId, cancellationToken));

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

    public async Task DeleteAsync(Guid actorId, CancellationToken cancellationToken)
    {
        var actor = await RequireById(actorId, cancellationToken);
        await _actors.DeleteAsync(actor);
    }

    public async Task<ActorVM> UpdateAsync(Guid actorId, ActorDto model, CancellationToken cancellationToken = default)
    {
        var actor = await RequireById(actorId, cancellationToken);

        actor.Update(model.Firstname, model.Lastname);

        await _actors.UpdateAsync(actor);

        return await GetByIdAsync(actor.Id, cancellationToken);
    }

    private async Task<ActorEntity> RequireById(Guid actorId, CancellationToken cancellationToken)
        => await _actors.FindByIdAsync(actorId, cancellationToken)
            ?? throw new ArgumentException("Actor not found");
}