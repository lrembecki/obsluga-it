namespace lrembecki.core.Helpers
{
    public interface IDateProvider
    {
        DateTime UtcNow { get; }
        DateTime Today { get; }
    }
}
