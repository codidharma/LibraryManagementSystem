using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed record Gender : ValueObject
{
    public string Value { get; }
    public Gender(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException("Gender value cannot be null, empty or whitespace string.");
        }
        Value = value;
    }
}
