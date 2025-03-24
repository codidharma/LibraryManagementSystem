using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain;

public sealed record PatronId(Guid Id) : ValueObject
{
    public Guid Value { get; } = Id;

}
