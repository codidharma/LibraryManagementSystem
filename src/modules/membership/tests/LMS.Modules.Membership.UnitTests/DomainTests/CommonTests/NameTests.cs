using LMS.Modules.Membership.Domain.Common;

namespace LMS.Modules.Membership.UnitTests.DomainTests.CommonTests;

public class NameTests : TestBase
{
    [Fact]
    public void Create_ShouldReturn_NameResult()
    {
        //Arrange
        string fullName = Faker.Person.FullName;

        //Act
        Result<Name> nameResult = Name.Create(fullName);

        //Assert
        Assert.True(nameResult.IsSuccess);
        Assert.False(nameResult.IsFailure);

        Name name = nameResult.Value;
        Assert.Equal(fullName, name.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidNameValue_ShouldReturn_FailureResult(string fullName)
    {
        //Arrange
        string expectedErrorMessage = "Name can not be null, empty or whitespace string.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        //Arrange
        Result<Name> nameResult = Name.Create(fullName);

        //Assert
        Assert.True(nameResult.IsFailure);
        Assert.False(nameResult.IsSuccess);
        Error error = nameResult.Error;

        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

}
