using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;


namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record DocumentContent : ValueObject
{
    public string Value { get; }
    public DocumentContent(string value)
    {
        if (!IsValidBase64String(value))
        {
            throw new InvalidValueException($"The value provided is not a valid base64 string.");
        }
        Value = value;
    }

    private static bool IsValidBase64String(string value)
    {
        var buffer = new Span<byte>(new byte[value.Length]);
        return Convert.TryFromBase64String(value, buffer, out int _);
    }
}
