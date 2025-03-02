using LMS.Modules.IAM.Domain.Users;
using LMS.Modules.IAM.UnitTests.Base;

namespace LMS.Modules.IAM.UnitTests.Users;

public class EmailTests : TestBase
{
    [Fact]
    public void WhenValidEmailValueIsPassed_ThenEmailInstanceIsCreated()
    {
        //Arrange
        string emailValue = Faker.Person.Email;
        Email email = new(emailValue);

        Assert.Equal(emailValue, email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("someValue")]
    public void WhenInvalidEmailValueIsPassed_ThenInvalidEmailExceptionIsThrown(string emailValue)
    {
        //Arrange
        Email email;

        //Act
        Action action = () => { email = new(emailValue); };

        Assert.Throws<InvalidEmailException>(action);

    }
}
