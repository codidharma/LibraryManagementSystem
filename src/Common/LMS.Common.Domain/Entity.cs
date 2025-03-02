namespace LMS.Common.Domain;

public abstract class Entity
{
    private readonly List<IDomianEvent> _domainEvents = [];

    protected Entity()
    { }
    public IReadOnlyCollection<IDomianEvent> DomianEvents => _domainEvents;

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public void Raise(IDomianEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}
