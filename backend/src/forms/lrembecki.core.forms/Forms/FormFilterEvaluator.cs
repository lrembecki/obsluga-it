using lrembecki.core.Services;

namespace lrembecki.core.forms.Forms;

internal sealed class FormFilterEvaluator : IFilterEvaluator<FormEntity, FormFilter>
{
    public IQueryable<FormEntity> Evaluate(IQueryable<FormEntity> query, FormFilter filter)
    {
        if (filter.FormDefinitionId.HasValue)
        {
            query = query.Where(e => e.FormDefinitionId == filter.FormDefinitionId);
        }

        if (filter.CreatedAt?.From is not null)
        {
            query = query.Where(e => e.CreatedAt >= filter.CreatedAt.From);
        }

        if (filter.CreatedAt?.To is not null)
        {
            query = query.Where(e => e.CreatedAt <= filter.CreatedAt.To);
        }

        return query;
    }
}
