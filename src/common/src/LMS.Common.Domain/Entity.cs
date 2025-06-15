namespace LMS.Common.Domain;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvents = [];
    public EntityId Id { get; private set; }
    protected Entity()
    {
        Id = new(Guid.NewGuid());
    }

    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
