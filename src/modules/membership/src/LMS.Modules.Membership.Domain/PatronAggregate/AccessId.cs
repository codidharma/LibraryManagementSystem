using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record AccessId : ValueObject
{
    public Guid Value { get; }

    private AccessId(Guid id)
    {
        Value = id;
    }

    public static Result<AccessId> Create(Guid value)
    {
        if (value == Guid.Empty)
        {
            Error error = Error.InvalidDomain(
                "Membership.InvalidDomainValue",
                "Invalid AccessId. Guid can not comprise of zeros only.");

            return Result.Failure<AccessId>(error);
        }

        AccessId accessId = new(value);
        return Result.Success(accessId);
    }
}
