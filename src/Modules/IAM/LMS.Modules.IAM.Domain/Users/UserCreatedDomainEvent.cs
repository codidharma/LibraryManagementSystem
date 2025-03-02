using LMS.Common.Domain;

namespace LMS.Modules.IAM.Domain.Users;

public sealed class UserCreatedDomainEvent : DomainEvent
{
    public UserCreatedDomainEvent(Guid id) : base(id) { }
    public UserCreatedDomainEvent(Guid id, DateTime occuredOnUtc) : base(id, occuredOnUtc) { }

}
