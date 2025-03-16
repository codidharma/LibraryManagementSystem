using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed record DocumentContent : ValueObject
{
    public string Value { get; }
    public DocumentContent(string value)
    {
        if (!IsValidBase64String(value))
        {
            throw new InvalidValueException($"The value provided for parameter {nameof(value)} is not a valid base64 string.");
        }

        Value = value;

    }

    private static bool IsValidBase64String(string value)
    {
        Span<byte> buffer = new Span<byte>(new byte[value.Length]);
        return Convert.TryFromBase64String(value, buffer, out int _);
    }
}
