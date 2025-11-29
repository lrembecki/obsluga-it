namespace lrembecki.core.Markers;

#pragma warning disable S3246 // Generic type parameters should be co/contravariant when possible
public interface IHasId<T>
#pragma warning restore S3246 // Generic type parameters should be co/contravariant when possible
{
    T Id { get; }
}