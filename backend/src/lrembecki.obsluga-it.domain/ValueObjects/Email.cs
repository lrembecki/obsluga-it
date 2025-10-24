using System.Text.RegularExpressions;

namespace lrembecki.obsluga_it.domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex = new Regex(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Address { get; }

    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address) || !IsValidEmail(address))
        {
            throw new ArgumentException("Invalid email address.", nameof(address));
        }

        Address = address;
    }

    private static bool IsValidEmail(string email)
    {
        return EmailRegex.IsMatch(email);
    }

    public override string ToString()
    {
        return Address;
    }

    public override bool Equals(object? obj)
    {
        return obj is Email email && Address.Equals(email.Address, StringComparison.OrdinalIgnoreCase);
    }

    public override int GetHashCode()
    {
        return Address.ToLowerInvariant().GetHashCode();
    }
}