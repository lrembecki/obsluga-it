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
    protected override async Task UpdateEntity(PermissionGroupEntity entity, PermissionGroupDto model)
    {
        entity.ClearPermissions();
        (await _permissions.GetAsync(e => model.Permissions.Contains(e.Id))).ForEach(entity.AddPermission);

        await base.UpdateEntity(entity, model);
    }
}

