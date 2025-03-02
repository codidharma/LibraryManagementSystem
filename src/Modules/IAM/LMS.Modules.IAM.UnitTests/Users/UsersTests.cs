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


        //Act
        var user = User.Create(name, email, identityId);

        //Assert
        Assert.NotNull(user);
        Assert.Equal(name, user.Name);
        Assert.Equal(email, user.Email);
        Assert.Equal(identityId, user.IdentityId);
        Assert.IsType<UserId>(user.Id);
    }

}
