using LMS.Common.Domain;

namespace LMS.Modules.IAM.Domain.Users;

public sealed class User : Entity
{
    public Name Name { get; }
    public Email Email { get; }

    public IdentityId IdentityId { get; }

    public UserId Id { get; }

    public Role Role { get; }

    private User()
    {
    }

    private User(Name name, Email email, IdentityId identityId, Role role)
    {
        Name = name;
        Email = email;
        IdentityId = identityId;
        Id = new(Guid.NewGuid());
        Role = role;
    }

    public static User Create(Name name, Email email, IdentityId identityId, Role role)
    {
        var user = new User(name, email, identityId, role);

        user.Raise(new UserCreatedDomainEvent(user.Id.Value));

        return user;
    }
}
