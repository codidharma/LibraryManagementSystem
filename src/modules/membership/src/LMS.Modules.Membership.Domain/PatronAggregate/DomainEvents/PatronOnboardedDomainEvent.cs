using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;

public sealed class PatronOnboardedDomainEvent : DomainEvent
{
    public string PatronType { get; init; }
    public string PatronName { get; init; }
    public string PatronEmail { get; init; }
    public Guid PatronId { get; init; }
    public PatronOnboardedDomainEvent(
        Guid id,
        DateTime occuredOnUtc,
        string patronType,
        string patronName,
        string patronEmail,
        Guid patronId) : base(id, occuredOnUtc)
    {
        PatronType = patronType;
        PatronName = patronName;
        PatronEmail = patronEmail;
        PatronId = patronId;
    }
}
