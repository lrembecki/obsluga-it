using lrembecki.obsluga_it.application.Abstractions;
using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Common;
using lrembecki.obsluga_it.domain.Entities;

namespace lrembecki.obsluga_it.application.Services;

public interface IActorService : ICrudService<ActorDto, ActorVM>;
internal sealed class ActorService(IUnitOfWork uow) : IActorService
{
    private readonly IRepository<ActorEntity> _actors = uow.GetRepository<ActorEntity>();

    public Task<List<ActorVM>> GetAllAsync(CancellationToken cancellationToken = default)
        => _actors.SelectAsync(e => ActorVM.MapFromDomainEntity(e));

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
            ?? throw new NullReferenceException("Actor not found");
}

public record FileBlobDto : BlobDto
{
    public string DisplayName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int Position { get; set; }
    public Guid? FileGroupId { get; set; }
}

public record BlobDto
{
    public Guid? Id { get; set; }
    public string? BinaryData { get; set; }
    public string? Filename { get; set; }
    public string? BlobUrl { get; set; }
    public string? BlobPath { get; set; }
    public long? Size { get; set; }
}

internal sealed class FileBlobService(IUnitOfWork uow, IBlobService blobService) : ICrudService<FileBlobDto, FileBlobVM>
{
    private readonly IRepository<FileBlobEntity> _files = uow.GetRepository<FileBlobEntity>();

    public async Task<FileBlobVM> CreateAsync(FileBlobDto model, CancellationToken cancellationToken = default)
    {
        model = await blobService.SyncBlobDataAsync(model, "files", cancellationToken);

        var fileBlob = BlobBaseEntity.Create<FileBlobEntity>(
            id: model.Id!.Value, 
            filename: model.Filename, 
            blobUrl: model.BlobUrl, 
            blobPath: model.BlobPath, 
            size: model.Size
        );

        fileBlob.Update(
            description: model.Description, 
            displayName: model.DisplayName, 
            position: model.Position, 
            group: model.FileGroupId is null
                ? null
                : await uow.GetRepository<FileGroupEntity>().FindByIdAsync(model.FileGroupId.Value, cancellationToken)
        );

        await _files.AddAsync(fileBlob);

        return await GetByIdAsync(fileBlob.Id, cancellationToken);
    }

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<FileBlobVM>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileBlobVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<FileBlobVM> UpdateAsync(Guid id, FileBlobDto model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}