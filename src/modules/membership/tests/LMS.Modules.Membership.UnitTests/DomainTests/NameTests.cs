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
        //Arrange
        string expectedExceptionMessage = "Name can not be null, empty or whitespace string.";
        Name name;

        //Act
        Action action = () => { name = new(nameValue); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);

    }
}
