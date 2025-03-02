using LMS.Modules.IAM.Domain.Users;
using LMS.Modules.IAM.UnitTests.Base;

namespace LMS.Modules.IAM.UnitTests.Users;

public class NameTests : TestBase
{
    [Fact]
    public void WhenNonEmptyFirstAndLastNamesAreProvided_ThenNameIsConstructed()
    {
        //Arrange
        string firstName = Faker.Person.FirstName;
        string lastName = Faker.Person.LastName;
        string expected = firstName + " " + lastName;
        //Act
        Name name = new(firstName, lastName);

        //Assert
        Assert.Equal(expected, name.Value);
    }

    [Theory]
    [InlineData("", "Henderson")]
    [InlineData("John", "")]
    [InlineData("", "")]
    public void WhenInvalidNamesArePassed_ThenConstructorThrowsException(string firstName, string lastName)
    {
        //Arrange
        Name name;
        //Act

        Action action = () => { name = new Name(firstName, lastName); };

        Assert.Throws<InvalidNameException>(action);

    }
}
