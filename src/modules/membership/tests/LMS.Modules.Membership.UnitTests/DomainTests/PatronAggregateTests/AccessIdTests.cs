namespace LMS.Modules.Membership.UnitTests.DomainTests.PatronAggregateTests;

public class AccessIdTests
{
    [Fact]
    public void Create_ShouldReturn_SuccessResult()
    {
        //Arrange
        var value = Guid.NewGuid();

        //Act
        Result<AccessId> accessIdResult = AccessId.Create(value);

        //Assert
        Assert.True(accessIdResult.IsSuccess);
        Assert.False(accessIdResult.IsFailure);

        AccessId accessId = accessIdResult.Value;

        Assert.Equal(value, accessId.Value);

    }

    [Fact]
    public void ForEmptyGuid_Create_ShouldReturn_FailureResult()
    {
        //Arrange
        string expectedErrorCode = "Membership.InvalidDomainValue";
        string expectedErrorMessage = "Invalid AccessId. Guid can not comprise of zeros only.";
        Guid value = Guid.Empty;

        //Act
        Result<AccessId> accessIdResult = AccessId.Create(value);

        //Assert
        Assert.True(accessIdResult.IsFailure);
        Assert.False(accessIdResult.IsSuccess);

        Error error = accessIdResult.Error;

        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }
}
