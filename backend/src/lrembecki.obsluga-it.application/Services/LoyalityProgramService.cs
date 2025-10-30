using lrembecki.obsluga_it.application.Contracts.Dtos;
using lrembecki.obsluga_it.application.Contracts.ViewModels;
using lrembecki.obsluga_it.domain.Entities;
using lrembecki.shared.application.Abstractions;
using lrembecki.shared.application.Extensions;

namespace lrembecki.obsluga_it.application.Services;

public interface ILoyalityProgramService : ICrudService<LoyalityProgramDto, LoyalityProgramVM>;
internal sealed class LoyalityProgramService(IUnitOfWork uow, IBlobService blobService) : ILoyalityProgramService
{
    private readonly IRepository<LoyalityProgramEntity> _loyalityPrograms = uow.GetRepository<LoyalityProgramEntity>();
    private readonly IRepository<ImageBlobEntity> _images = uow.GetRepository<ImageBlobEntity>();

    public async Task<LoyalityProgramVM> CreateAsync(LoyalityProgramDto model, CancellationToken cancellationToken = default)
    {
        model.Image = await blobService.SyncBlobDataAsync(model.Image, "images", cancellationToken);

        var imageBlob = BlobBaseEntity.Create<ImageBlobEntity>(
        id: model.Image.Id!.Value,
        filename: model.Image.Filename,
        blobUrl: model.Image.BlobUrl,
        blobPath: model.Image.BlobPath,
        size: model.Image.Size
        );

        imageBlob.Update(model.Image.DisplayName, model.Image.Description, model.Image.Width, model.Image.Height, []);
        await _images.AddAsync(imageBlob);

        var loyalityProgram = LoyalityProgramEntity.Create(Guid.NewGuid(), model.Name, model.Title, model.Description, imageBlob);
        await _loyalityPrograms.AddAsync(loyalityProgram);

        return await GetByIdAsync(loyalityProgram.Id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var loyalityProgram = await _loyalityPrograms.RequireByIdAsync(id, cancellationToken);
        await _loyalityPrograms.DeleteAsync(loyalityProgram);
    }

    public Task<List<LoyalityProgramVM>> GetAllAsync(CancellationToken cancellationToken = default)
    => _loyalityPrograms.SelectAsync(e => LoyalityProgramVM.MapFromDomainEntity(e));

    public async Task<LoyalityProgramVM> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    => LoyalityProgramVM.MapFromDomainEntity(await _loyalityPrograms.RequireByIdAsync(id, cancellationToken));

    public async Task<LoyalityProgramVM> UpdateAsync(Guid id, LoyalityProgramDto model, CancellationToken cancellationToken = default)
    {
        var loyalityProgram = await _loyalityPrograms.RequireByIdAsync(id, cancellationToken);

        if (model.Image.BinaryData is not null)
        {
            await blobService.RemoveBlobAsync(loyalityProgram.Image.BlobPath!, "images", cancellationToken);
            model.Image = await blobService.SyncBlobDataAsync(model.Image, "images", cancellationToken);
            loyalityProgram.Image.Update(model.Image.Filename, model.Image.BlobUrl, model.Image.BlobPath, model.Image.Size);
            loyalityProgram.Image.Update(model.Image.DisplayName, model.Image.Description, model.Image.Width, model.Image.Height, []);
        }
        else
        {
            loyalityProgram.Image.Update(model.Image.DisplayName, model.Image.Description, model.Image.Width, model.Image.Height, []);
        }

        loyalityProgram.Update(model.Name, model.Title, model.Description, loyalityProgram.Image);

        await _loyalityPrograms.UpdateAsync(loyalityProgram);

        return await GetByIdAsync(loyalityProgram.Id, cancellationToken);
    }
}
