using System.Net.Mail;
using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record Email : ValueObject
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value;

    }
    public static Result<Email> Create(string value)
    {
        if (!IsValid(value))
        {
            Error error = Error.InvalidDomain(
                "Membership.InvalidDomainValue",
                "Email should be in format abc@pqr.com.");

            return Result.Failure<Email>(error);
        }

        Email email = new(value);
        return Result.Success<Email>(email);
    }

    private static bool IsValid(string value)
    {
        return MailAddress.TryCreate(value, out _);
    }
}
