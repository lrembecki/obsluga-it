using lrembecki.core.account.Dtos;
using lrembecki.core.account.Entities;
using lrembecki.core.account.ViewModels;
using lrembecki.core.Services;

namespace lrembecki.core.account.Services;


public interface IPermissionService : ICrudService<PermissionDto, PermissionVM>;
internal class PermissionService(IUnitOfWork uow) : BaseCrudService<PermissionEntity, PermissionVM, PermissionDto>(uow), IPermissionService;
