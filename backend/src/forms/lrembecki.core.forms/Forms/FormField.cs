namespace lrembecki.core.forms.Forms;

public class FormFieldEntity
{
    public Guid FormId { get; private set; }
    public string Name { get; private set; }
    public string Value { get; private set; } = null!;

    public static FormFieldEntity Create(Guid FormId, string name, string Value)
        => new FormFieldEntity
        {
            FormId = FormId,
            Name = name,
            Value = Value
        };
}