namespace lrembecki.core.Services;

public interface IFilterEvaluator<TEntity, TFilter>
{
    public IQueryable<TEntity> Evaluate(IQueryable<TEntity> query, TFilter filter);
}