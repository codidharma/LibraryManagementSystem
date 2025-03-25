using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record AccessId(Guid id) : ValueObject
{
    public Guid Value { get; } = id;
}
