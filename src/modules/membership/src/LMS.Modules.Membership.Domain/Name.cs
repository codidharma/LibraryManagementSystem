using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Exceptions;


namespace LMS.Modules.Membership.Domain;

public sealed record Name : ValueObject
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
