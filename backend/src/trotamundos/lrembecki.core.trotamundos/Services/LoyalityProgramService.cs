using lrembecki.core.Services;
using lrembecki.core.storage.Services;
using lrembecki.core.trotamundos.Dtos;
using lrembecki.core.trotamundos.Entitites;
using lrembecki.core.trotamundos.ViewModels;

namespace lrembecki.core.trotamundos.Services;

public interface ILoyalityProgramService : ICrudService<LoyalityProgramDto, LoyalityProgramVM>;
internal sealed class LoyalityProgramService(IUnitOfWork uow, IStorageService storage) : BaseCrudService<LoyalityProgramEntity, LoyalityProgramVM, LoyalityProgramDto>(uow), ILoyalityProgramService
{
    protected override async Task DeleteEntity(LoyalityProgramEntity entity, CancellationToken cancellationToken)
    {
        await base.DeleteEntity(entity, cancellationToken);
        await storage.DeleteAsync(entity.ImageId, cancellationToken);
    }

    protected override async Task<LoyalityProgramEntity> CreateEntity(Guid id, LoyalityProgramDto model, CancellationToken cancellationToken)
    {
        var storageVM = await storage.CreateAsync(model.Image);
        model = model with { ImageId = storageVM.Id };

        var entity = await base.CreateEntity(id, model, cancellationToken);
        return entity;
    }

    protected override async Task UpdateEntity(LoyalityProgramEntity entity, LoyalityProgramDto model)
    {
        var storageVM = await storage.UpdateAsync(entity.ImageId, model.Image);

        await base.UpdateEntity(entity, model);
    }
}
