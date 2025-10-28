using lrembecki.obsluga_it.domain.Abstractions;
using System.Linq.Expressions;

namespace lrembecki.obsluga_it.application.Contracts.Specifications;

public static class SpecificationExtensions
{
    public static Specification<T> WithSubscriptionId<T>(this Specification<T> specification, Guid? subscriptionId)
        where T : IHasSubscriptionId
    {
        specification ??= Specification<T>.All;

        if (subscriptionId == null) return specification!;

        return specification.And(new Specification<T>(item => item.SubscriptionId == subscriptionId));
    }

    public static Specification<T> ReflectWithSubscriptionId<T>(this Specification<T> specification, Guid? subscriptionId)
    {
        if (typeof(T).GetInterfaces().Contains(typeof(IHasSubscriptionId)))
        {
            specification = (typeof(SpecificationExtensions)
                .GetMethod(nameof(SpecificationExtensions.WithSubscriptionId), System.Reflection.BindingFlags.Static)!
                .Invoke(null, new object[] { specification, subscriptionId! })
                as Specification<T>)!;
        }

        return specification;
    }
}

public record Specification<T>(
    Expression<Func<T, bool>> Predicate
)
{
    public static Specification<T> All => new Specification<T>(item => true);

    public static Specification<T> Filter(Expression<Func<T, bool>> predicate)
        => new Specification<T>(predicate);

    public Specification<T> And(Specification<T> specification)
        => new Specification<T>(specification);

    public Specification<T, VM> WithProject<VM>(Expression<Func<T, VM>> project)
        => new Specification<T, VM>(Predicate, project);
}

public record Specification<T, VM>(
    Expression<Func<T, bool>> Predicate,
    Expression<Func<T, VM>> Project
) : Specification<T>(Predicate);