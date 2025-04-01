using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;

public sealed class PatronCreatedDomainEvent : DomainEvent
{
    public string PatronType { get; init; }
    public PatronCreatedDomainEvent(Guid id, DateTime occuredOnUtc, string patronType) : base(id, occuredOnUtc)
    {
        PatronType = patronType;
    }
}
