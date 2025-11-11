namespace lrembecki.core.Markers;

public interface IHasAuditFields
{
    DateTime CreatedAt { get; }
    Guid? CreatedById { get; }
    DateTime UpdatedAt { get; }
    Guid? UpdatedById { get; }
}
