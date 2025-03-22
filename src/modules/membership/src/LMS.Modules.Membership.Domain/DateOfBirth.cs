using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Exceptions;


namespace LMS.Modules.Membership.Domain;

public sealed record DateOfBirth : ValueObject
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
