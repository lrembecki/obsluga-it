namespace lrembecki.obsluga_it.domain.Abstractions;

public interface IHasAuditFields
{
    DateTime CreatedAt { get; set; }
    Guid? CreatedById { get; set; }
    DateTime UpdatedAt { get; set; }
    Guid? UpdatedById { get; set; }
}
