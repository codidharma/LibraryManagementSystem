using System.Text.RegularExpressions;
using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record NationalId : ValueObject
{
    private const string errorDescription = $"The value of {nameof(NationalId)} should be a valid alpha numeric ten lettered string with only capital letters.";
    private static readonly Error error = Error.InvalidDomain("Membership.InvalidDomainValue", errorDescription);
    public string Value { get; }
    private NationalId(string value)
    {
        Value = value;
    }

    public static Result<NationalId> Create(string value)
    {
        if (!IsValid(value))
        {
            return Result.Failure<NationalId>(error);
        }

        NationalId nationalId = new(value);
        return Result.Success(nationalId);
    }

    private static bool IsValid(string value)
    {
        int maxLength = 10;
        string validIdPattern = "^[A-Z0-9]+$";
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }

        if (value.Length > maxLength)
        {
            return false;
        }

        return Regex.IsMatch(value, validIdPattern);
    }
}
