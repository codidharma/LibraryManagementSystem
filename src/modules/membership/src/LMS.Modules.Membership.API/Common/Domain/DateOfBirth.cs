using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed record DateOfBirth : ValueObject
{
    public DateTime Value { get; }
    public DateOfBirth(DateTime value)
    {
        if (IsInFuture(value))
        {
            throw new InvalidValueException("Date of birth cannot be in future.");
        }
        Value = value;
    }

    private static bool IsInFuture(DateTime value)
    {
        return value.Date > DateTime.Today.Date;

    }
}
