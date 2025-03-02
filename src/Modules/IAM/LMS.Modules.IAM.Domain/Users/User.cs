using LMS.Common.Domain;

namespace LMS.Modules.IAM.Domain.Users;

public sealed class User : Entity
{
    public Name Name { get; }
    public Email Email { get; }

    public IdentityId IdentityId { get; }

    public UserId Id { get; }

    private User()
    {
    }

    private User(Name name, Email email, IdentityId identityId)
    {
        Name = name;
        Email = email;
        IdentityId = identityId;
        Id = new(Guid.NewGuid());
    }

    public static User Create(Name name, Email email, IdentityId identityId)
    {
        return new User(name, email, identityId);
    }
}
