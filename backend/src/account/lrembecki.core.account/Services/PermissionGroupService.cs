using lrembecki.core.account.Dtos;
using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Services;

namespace lrembecki.core.account.Services;

public interface IPermissionGroupService : ICrudService<PermissionGroupDto, PermissionGroupVM>;
internal class PermissionGroupService(IUnitOfWork uow) 
    : BaseCrudService<PermissionGroupEntity, PermissionGroupVM, PermissionGroupDto>(uow), IPermissionGroupService
{
    private readonly IRepository<PermissionEntity> _permissions = uow.GetRepository<PermissionEntity>();

    protected override async Task<PermissionGroupEntity> CreateEntity(Guid id, PermissionGroupDto model, CancellationToken cancellationToken)
    {
        var entity = await base.CreateEntity(id, model, cancellationToken);

        await AddPermissions(model, entity);

        return entity;
    }

    protected override async Task UpdateEntity(PermissionGroupEntity entity, PermissionGroupDto model)
    {
        await AddPermissions(model, entity);

        entity.Permissions.Where(e => !model.Permissions.Contains(e.Id))
            .ToList().ForEach(e => entity.Permissions.Remove(e));

        await base.UpdateEntity(entity, model);
    }

    private async Task AddPermissions(PermissionGroupDto model, PermissionGroupEntity entity)
    {
        var toAdd = model.Permissions.Where(e => !entity.Permissions.Select(y => y.Id).Contains(e)).ToList();
        if (toAdd.Count > 0)
        {
            entity.Permissions.AddRange(
                await _permissions.GetAsync(e => toAdd.Contains(e.Id))
            );
        }
    }
}

