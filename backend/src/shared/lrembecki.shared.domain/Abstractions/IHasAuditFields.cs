namespace lrembecki.shared.domain.Abstractions;

public interface IHasAuditFields
{
    DateTime CreatedAt { get; }
    Guid? CreatedById { get; }
    DateTime UpdatedAt { get; }
    Guid? UpdatedById { get; }
}
