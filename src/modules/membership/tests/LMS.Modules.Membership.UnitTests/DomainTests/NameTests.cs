using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class NameTests : TestBase
{
    [Fact]
    public void Constructor_Returns_NameInstance()
    {
        //Arrange
        string fullName = Faker.Person.FullName;

        //Act
        Name name = new(fullName);

        //Assert
        Assert.Equal(fullName, name.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidNameValue_Should_ThrowInvalidValueException(string nameValue)
    {
        //Act
        Name name;

        Action action = () => { name = new(nameValue); };

        //Assert
        Assert.Throws<InvalidValueException>(action);

    }
}
