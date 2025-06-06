using LMS.Common.Domain;


namespace LMS.Modules.Membership.Domain.Common;

public sealed record Name : ValueObject
{
    private Name(string value)
    {
        Value = value;
    }

    public static Result<Name> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            var InvalidNameError = Error.InvalidDomain("Membership.InvalidDomainValue", "Name can not be null, empty or whitespace string.");
            return Result.Failure<Name>(InvalidNameError);

        }
        Name name = new(value);
        return Result.Success(name);
    }
    public string Value { get; }
}
