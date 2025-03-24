using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DateofBirthTests : TestBase
{
    [Fact]
    public void Constructor_ShouldReturn_Value()
    {
        //Arrange
        DateTime dateOfBirthValue = Faker.Person.DateOfBirth;

        //Act
        DateOfBirth dateOfBirth = new(dateOfBirthValue);

        //Assert
        Assert.Equal(dateOfBirthValue, dateOfBirth.Value);
    }

    [Fact]
    public void DateOfBirth_ShouldNotBe_InFuture()
    {
        //Arrange
        string expectedExceptionMessage = "Date of birth cannot be in future or today.";
        DateTime futureDateOfBirthValue = DateTime.UtcNow.AddYears(1000);

        //Act
        DateOfBirth dateOfBirth;

        Action action = () => { dateOfBirth = new(futureDateOfBirthValue); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void DateOfBirth_ShouldNotBe_Today()
    {
        //Arrange
        string expectedExceptionMessage = "Date of birth cannot be in future or today.";
        DateTime futureDateOfBirthValue = DateTime.Now;

        //Act
        DateOfBirth dateOfBirth;

        Action action = () => { dateOfBirth = new(futureDateOfBirthValue); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}
