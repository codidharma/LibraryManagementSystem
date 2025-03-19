using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed record DateOfBirth : ValueObject
{
    public DateTime Value { get; }
    public DateOfBirth(DateTime value)
    {
        if (IsDateOfBirthInFutureorToday(value))
        {
            throw new InvalidValueException("Date of birth cannot be in future or today.");
        }
        Value = value;
    }

    private static bool IsDateOfBirthInFutureorToday(DateTime value)
    {

        return value.Date >= DateTime.Today.Date;
    }
}
