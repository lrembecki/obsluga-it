using lrembecki.core.Helpers;

namespace lrembecki.infrastructure.Helpers;

internal class DateProvider : IDateProvider
{
    public DateTime UtcNow => DateTime.UtcNow;

    public DateTime Today => DateTime.Today.Date;
}
