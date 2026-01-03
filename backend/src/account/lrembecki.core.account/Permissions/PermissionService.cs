using lrembecki.core.Services;

namespace lrembecki.core.account.Permissions;


public interface IPermissionService : ICrudService<PermissionDto, PermissionVM>;
internal class PermissionService(IUnitOfWork uow) : BaseCrudService<PermissionEntity, PermissionVM, PermissionDto>(uow), IPermissionService;
