using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed record Name : ValueObject
{
    public Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new InvalidValueException("Name can not be null, empty or whitespace string.");
        }
        Value = value;
    }
    public string Value { get; }
}
