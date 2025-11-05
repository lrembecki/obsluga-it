using lrembecki.core.Helpers;
using lrembecki.core.Markers;
using System.Linq.Expressions;

namespace lrembecki.core.GlobalFilters;

public class SubscriptionIdGlobalFilter(ISessionAccessor sessionAccessor) : IGlobalFilter
{
    private bool _enabled = true;
    public void Disable() => _enabled = false;

    public void Enable() => _enabled = true;

    public IQueryable<T> Evaluate<T>(IQueryable<T> query)
    {
        if (typeof(T).IsAssignableTo(typeof(IHasSubscriptionId)) && _enabled)
        {
            var subscriptionId = sessionAccessor.SubscriptionId;

            var subscriptionPredicate = (Expression<Func<T, bool>>)GetType()
                .GetMethod(nameof(SubscriptionIdPredicate))!
                .MakeGenericMethod(typeof(T)).Invoke(this, [])!;

            query = query.Where(subscriptionPredicate);
        }

        return query;
    }

    public Expression<Func<Y, bool>> SubscriptionIdPredicate<Y>()
        where Y : IHasSubscriptionId
        => item => item.SubscriptionId == sessionAccessor.SubscriptionId;
}
