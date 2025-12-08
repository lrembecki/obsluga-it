using lrembecki.core.Services;

namespace lrembecki.core.forms.Forms;


public interface IFormService : ICrudService<FormDto, FormVM>;
public sealed class FormService(IUnitOfWork uow) 
    : BaseCrudService<FormEntity, FormVM, FormDto>(uow)
    , IFormService
{
    protected override async Task<FormEntity> CreateEntity(Guid id, FormDto model, CancellationToken cancellationToken)
    {
        var entity = await base.CreateEntity(id, model, cancellationToken);

        entity.Fields = model.Fields.ToList()
            .Select(f => FormFieldEntity.Create(entity.Id, f.Key, f.Value))
            .ToList();

        return entity;
    }
}