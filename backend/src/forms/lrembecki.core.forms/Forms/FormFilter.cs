namespace lrembecki.core.forms.Forms;

public record FormFilter
{
    public Guid? FormDefinitionId { get; set; }
    public DateRangeFilter? CreatedAt { get; set; }
}

public record DateRangeFilter
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
}