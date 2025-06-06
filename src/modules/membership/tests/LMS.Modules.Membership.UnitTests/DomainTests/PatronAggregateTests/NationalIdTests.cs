namespace LMS.Modules.Membership.UnitTests.DomainTests.PatronAggregateTests;

public class NationalIdTests
{
    [Fact]
    public void Create_ShouldReturn_NationalIdInstance()
    {
        //Arrange
        string nationalIdValue = "AZBC12345";

        //Act
        Result<NationalId> nationalIdResult = NationalId.Create(nationalIdValue);

        //Assert
        Assert.True(nationalIdResult.IsSuccess);
        Assert.False(nationalIdResult.IsFailure);
        NationalId nationalId = nationalIdResult.Value;
        Assert.Equal(nationalIdValue, nationalId.Value);
    }

    [Theory]
    [InlineData("ABC1234567891")]
    [InlineData("abc1234567")]
    [InlineData(" ")]
    [InlineData("")]
    public void ForInvalidNationalIdValue_Create_ShouldReturn_FailureResult(string value)
    {
        //Arrange
        string errorDescription = $"The value of {nameof(NationalId)} should be a valid alpha numeric ten lettered string with only capital letters.";
        string errorCode = "Membership.InvalidDomainValue";

        //Act
        Result<NationalId> nationalIdResult = NationalId.Create(value);

        //Assert
        Assert.True(nationalIdResult.IsFailure);
        Assert.False(nationalIdResult.IsSuccess);

        Error error = nationalIdResult.Error;
        Assert.Equal(errorDescription, error.Description);
        Assert.Equal(errorCode, error.Code);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }
}
