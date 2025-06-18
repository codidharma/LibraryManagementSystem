namespace LMS.Common.Domain;

public abstract class DomainEvent : IDomainEvent
{
    public Guid Id { get; protected set; }
    public DateTime OccuredOnUtc { get; protected set; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccuredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }
}
