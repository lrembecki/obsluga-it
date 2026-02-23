namespace lrembecki.core.ValueTypes;

public record Price
{
    public decimal? Value { get; set; }
    public string? Currency { get; set; }
    public string? Description { get; set; }
}
