namespace LMS.Common.Domain.Tests.ResultsTests;

public class ResultTests
{
    [Fact]
    public void New_ShouldReturn_SuccessResult()
    {
        //Act
        Result result = new(true, Error.None);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void IfErrorTypeIsNotNoneForSuccess_Then_New_ShouldThrow_InvalidOperationException()
    {
        //Arrange
        string expectedErrorMEssage = "ErrorType should be None for success result.";
        Error error = Error.NullValue("The field x can not be null.");

        Result result;

        //Act
        Action action = () => { result = new(true, error); };

        //Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(expectedErrorMEssage, exception.Message);
    }

    [Fact]
    public void IfErrorTypeIsNoneForFailure_Then_New_ShouldThrow_InvalidOperationException()
    {
        //Arrange
        string expectedErrorMEssage = "ErrorType should not be None for failure result.";
        Error error = Error.None;

        Result result;

        //Act
        Action action = () => { result = new(false, error); };

        //Assert
        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(expectedErrorMEssage, exception.Message);
    }

    [Fact]
    public void Success_ShouldReturn_SuccessResultInstance()
    {
        //Act
        Result result = Result.Success();

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
    }

    [Fact]
    public void Failure_ShoudReturn_FailureResultIntsance()
    {
        //Arrange
        Error error = Error.NullValue("The field x can not be null.");

        //Act
        Result result = Result.Failure(error);

        //Assert
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }

    [Fact]
    public void SuccessForGenericResult_ShouldReturn_SuccessResultInstance()
    {
        //Arrange
        string expectedValue = "result";
        // Act
        Result<string> result = Result.Success<string>(expectedValue);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
        Assert.Equal(expectedValue, result.Value);
    }

    [Fact]
    public void FailureForGenericResult_ShouldReturn_FailureResultInstance()
    {
        //Arrange
        Error error = Error.NullValue("Field x can not be null.");

        // Act
        Result<string> result = Result.Failure<string>(error);

        //Assert
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);
    }
}
