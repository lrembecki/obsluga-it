using lrembecki.core.Services;
using lrembecki.core.settings.Entities;

namespace lrembecki.core.forms.FormDefinitions;

public interface IFormDefinitionService : ICrudService<FormDefinitionDto, FormDefinitionVM>;
internal sealed class FormDefinitionService(IUnitOfWork uow) 
    : BaseCrudService<FormDefinitionEntity, FormDefinitionVM, FormDefinitionDto>(uow)
    , IFormDefinitionService
{
    private IRepository<NotificationEntity> _notifications => _uow.GetRepository<NotificationEntity>();

    protected override async Task<FormDefinitionEntity> CreateEntity(Guid id, FormDefinitionDto model, CancellationToken cancellationToken)
    {
        model = await UpdateNotification(null, model);
        return await base.CreateEntity(id, model, cancellationToken);
    }
    protected override async Task UpdateEntity(FormDefinitionEntity entity, FormDefinitionDto model)
    {
        model = await UpdateNotification(entity, model);
        await base.UpdateEntity(entity, model);
    }

    private async Task<FormDefinitionDto> UpdateNotification(FormDefinitionEntity? entity, FormDefinitionDto model)
    {
        if (model.Notification is not null)
        {
            model = model with
            {
                NotificationId = (await (entity?.NotificationId != null
                    ? _notifications.UpdateAsync(entity.Notification!)
                    : _notifications.AddAsync(NotificationEntity.Create(Guid.NewGuid(), model.Notification))
                )).Id
            };
        }

        return model;
    }
}
