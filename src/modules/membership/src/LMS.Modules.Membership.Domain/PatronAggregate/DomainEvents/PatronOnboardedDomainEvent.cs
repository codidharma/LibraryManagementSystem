using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;

public sealed class PatronOnboardedDomainEvent : DomainEvent
{
    public string PatronType { get; init; }
    public PatronOnboardedDomainEvent(Guid id, DateTime occuredOnUtc, string patronType) : base(id, occuredOnUtc)
    {
        PatronType = patronType;
    }
}
