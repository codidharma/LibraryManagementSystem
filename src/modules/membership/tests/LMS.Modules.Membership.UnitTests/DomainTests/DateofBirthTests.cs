using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
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
        DateTime futureDateOfBirthValue = DateTime.UtcNow.AddYears(1000);

        //Act
        DateOfBirth dateOfBirth;

        Action action = () => { dateOfBirth = new(futureDateOfBirthValue); };

        //Assert
        Assert.Throws<InvalidValueException>(action);


    }
}
