using System.Net.Mail;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record Email : ValueObject
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value))
        {
            throw new InvalidValueException("Email should be in format abc@pqr.com.");
        }
        Value = value;

    }

    private static bool IsValid(string value)
    {
        return MailAddress.TryCreate(value, out _);
    }
}
