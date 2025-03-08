using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class EmailTests : TestBase
{
    [Fact]
    public void Constructor_ShouldReturn_EmailInstance()
    {
        //Arrange
        string emailValue = Faker.Person.Email;

        //Assert
        Email email = new(emailValue);

        //Assert
        Assert.Equal(emailValue, email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("abc@")]
    public void InvalidEmail_Throws_InvalidValueException(string emailValue)
    {
        //Arrange
        Email email;

        //Act
        Action action = () => { email = new(emailValue); };

        //Assert
        Assert.Throws<InvalidValueException>(action);
    }
}
