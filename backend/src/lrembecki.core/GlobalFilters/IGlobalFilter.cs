namespace lrembecki.core.GlobalFilters;

public interface IGlobalFilter
{
    IQueryable<T> Evaluate<T>(IQueryable<T> query);
    void Enable();
    void Disable();
}
