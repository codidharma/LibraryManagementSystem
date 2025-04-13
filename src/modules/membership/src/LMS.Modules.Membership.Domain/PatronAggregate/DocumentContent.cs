using LMS.Common.Domain;


namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record DocumentContent : ValueObject
{
    public string Value { get; }
    private DocumentContent(string value)
    {
        Value = value;
    }

    public static Result<DocumentContent> Create(string value)
    {
        if (!IsValidBase64String(value))
        {
            Error error = Error.InvalidDomain(
                "Membership.InvalidDomainValue",
                "The value provided is not a valid base64 string.");

            return Result.Failure<DocumentContent>(error);
        }

        DocumentContent documentContent = new(value);
        return Result.Success(documentContent);
    }

    private static bool IsValidBase64String(string value)
    {
        var buffer = new Span<byte>(new byte[value.Length]);
        return Convert.TryFromBase64String(value, buffer, out int _);
    }
}
