using lrembecki.core.Services;

namespace lrembecki.core.forms.FormDefinitions;

public interface IFormDefinitionService : ICrudService<FormDefinitionDto, FormDefinitionVM>;
internal sealed class FormDefinitionService(IUnitOfWork uow) 
    : BaseCrudService<FormDefinitionEntity, FormDefinitionVM, FormDefinitionDto>(uow)
    , IFormDefinitionService
{
    private IRepository<FormFieldDefinitionEntity> FieldRepository => _uow.GetRepository<FormFieldDefinitionEntity>();
    protected override async Task<FormDefinitionEntity> CreateEntity(Guid id, FormDefinitionDto model, CancellationToken cancellationToken)
    {
        var entity = await base.CreateEntity(id, model, cancellationToken);

        UpdateFields(model, entity);

        return entity;
    }

    protected override async Task UpdateEntity(FormDefinitionEntity entity, FormDefinitionDto model)
    {
        var fieldEntities = await FieldRepository
            .GetAll(e => e.FormDefinitionId == entity.Id)
            .ToAsyncEnumerable().ToListAsync();

        await FieldRepository.DeleteAsync(fieldEntities);

        UpdateFields(model, entity);

        await base.UpdateEntity(entity, model);
    }

    private static void UpdateFields(FormDefinitionDto model, FormDefinitionEntity entity)
    {
        foreach (var field in model.Fields)
        {
            entity.Fields.Add(FormFieldDefinitionEntity.Create(entity.Id, field));
        }
    }
}
