namespace LMS.Common.Domain.Tests.DomainEventTests;

public class DummyDomainEvent : DomainEvent
{
    public DummyDomainEvent(Guid id, DateTime occuredOnUtc) : base(id, occuredOnUtc)
    {

    }
    public DummyDomainEvent() : base() { }

}
