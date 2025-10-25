using System.Text.RegularExpressions;

namespace lrembecki.obsluga_it.domain.ValueObjects;

public partial class Phone
{
    [GeneratedRegex(@"^\+?[1-9]\d{1,14}$", RegexOptions.IgnoreCase)]
    private static partial Regex PhoneRegex();
    public string Number { get; }
    public Phone(string number)
    {
        if (string.IsNullOrWhiteSpace(number) || !IsValidPhone(number))
        {
            throw new ArgumentException("Invalid phone number.", nameof(number));
        }
        Number = number;
    }
    private static bool IsValidPhone(string phone)
    {
        return PhoneRegex().IsMatch(phone);
    }
    public override string ToString()
    {
        return Number;
    }
    public override bool Equals(object? obj)
    {
        return obj is Phone phone && Number.Equals(phone.Number, StringComparison.OrdinalIgnoreCase);
    }
    public override int GetHashCode()
    {
        return Number.ToLowerInvariant().GetHashCode();
    }
}
