
namespace LMS.Common.Domain;

public abstract class DomainEvent : IDomianEvent
{
    public Guid Id { get; init; }

    public DateTime OccuredOnUtc { get; init; }

    protected DomainEvent(Guid id)
    {
        Id = id;
        OccuredOnUtc = DateTime.UtcNow;
    }

    protected DomainEvent(Guid id, DateTime occuredOnUtc)
    {
        Id = id;
        OccuredOnUtc = occuredOnUtc;
    }
}
