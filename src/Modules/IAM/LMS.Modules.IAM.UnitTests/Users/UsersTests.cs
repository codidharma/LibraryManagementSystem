using LMS.Modules.IAM.Domain.Users;
using LMS.Modules.IAM.UnitTests.Base;
namespace LMS.Modules.IAM.UnitTests.Users;

public class UsersTests : TestBase
{
    [Fact]
    public void Create_ShouldReturn_User()
    {
        //Arrange
        string firstName = Faker.Person.FirstName;
        string lastName = Faker.Person.LastName;
        Name name = new(firstName, lastName);

        string emailValue = Faker.Person.Email;
        Email email = new(emailValue);

        IdentityId identityId = new(Guid.NewGuid());

        Role role = Role.Librarian;

        //Act
        var user = User.Create(name, email, identityId, role);

        //Assert
        Assert.NotNull(user);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(identityId, user.IdentityId);
        Assert.IsType<UserId>(user.Id);
        Assert.Equal(role, user.Role);


        UserCreatedDomainEvent userCreatedDomainEvent = GetRaisedEvent<UserCreatedDomainEvent>(user);

        Assert.Equal(user.Id.Value, userCreatedDomainEvent.Id);
    }

    [Fact]
    public void Create_Returns_RegularPatronUser()
    {
        //Arrange
        Name name = new(Faker.Person.FirstName, Faker.Person.LastName);
        Email email = new(Faker.Person.Email);
        IdentityId identityId = new(Guid.NewGuid());
        Role role = Role.RegularPatron;

        //Act
        var user = User.Create(name, email, identityId, role);

        //Assert
        Assert.Equal(role, user.Role);
    }

    [Fact]
    public void Create_Returns_ResearchPatronUser()
    {
        //Arrange
        Name name = new(Faker.Person.FirstName, Faker.Person.LastName);
        Email email = new(Faker.Person.Email);
        IdentityId identityId = new(Guid.NewGuid());
        Role role = Role.ResearchPatron;

        //Act
        var user = User.Create(name, email, identityId, role);

        //Assert
        Assert.Equal(role, user.Role);
    }
}
