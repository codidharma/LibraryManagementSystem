using System.Net.Mail;

namespace LMS.Modules.IAM.Domain.Users;

public sealed record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (!IsValid(value))
        {
            string errorMessage = $"Email should be in format abc@testdomain.com";
            throw new InvalidEmailException(errorMessage);
        }

        Value = value;
    }

    private static bool IsValid(string value)
    {
        return MailAddress.TryCreate(value, out _);
    }
}
