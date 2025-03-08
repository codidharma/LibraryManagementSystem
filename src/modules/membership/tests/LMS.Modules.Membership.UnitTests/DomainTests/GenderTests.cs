using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class GenderTests : TestBase
{
    [Fact]
    public void Constructor_Should_ReturnGender()
    {
        //Arrange
        string genderValue = Faker.Person.Gender.ToString();

        //Act
        Gender gender = new(genderValue);

        //Assert
        Assert.Equal(genderValue, gender.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidGender_Throws_InvalidValueException(string genderValue)
    {
        //Arrange
        Gender gender;

        //Act
        Action action = () => { gender = new(genderValue); };

        //Assert
        Assert.Throws<InvalidValueException>(action);

    }
}
