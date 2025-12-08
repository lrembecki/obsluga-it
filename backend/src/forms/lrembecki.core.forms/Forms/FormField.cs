namespace lrembecki.core.forms.Forms;

public class FormFieldEntity
{
    public Guid FormId { get; private set; }
    public Guid FormDefinitionFieldId { get; private set; }
    public string Value { get; private set; } = null!;

    public static FormFieldEntity Create(Guid FormId, Guid FormDefinitionFieldId, string Value)
        => new FormFieldEntity
        {
            FormId = FormId,
            FormDefinitionFieldId = FormDefinitionFieldId,
            Value = Value
        };
}