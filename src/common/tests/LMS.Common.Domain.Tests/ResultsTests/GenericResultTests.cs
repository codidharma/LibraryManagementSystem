namespace LMS.Common.Domain.Tests.ResultsTests;

public class GenericResultTests
{
    [Fact]
    public void New_ShouldReturn_SuccessResult()
    {
        //Arrange
        Guid guid = Guid.NewGuid();

        //Act
        Result<Guid> result = new(guid, true, Error.None);

        //Assert
        Assert.True(result.IsSuccess);
        Assert.False(result.IsFailure);
        Assert.Equal(Error.None, result.Error);
        Assert.Equal(guid, result.Value);
    }

    [Fact]
    public void AccessingValueForFailure_ShouldThrow_InvalidOperatopnException()
    {
        //Arrange
        string expectedErrorMessage = $"The property Value cannot be access when IsFailure is true.";
        Error error = Error.NullValue("Field x can not be null");
        int valueFromResult;

        //Act
        Result<int> result = new(default, false, error);

        Action action = () => { valueFromResult = result.Value; };

        //Assert
        Assert.True(result.IsFailure);
        Assert.False(result.IsSuccess);
        Assert.Equal(error, result.Error);

        InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(expectedErrorMessage, exception.Message);

    }
}
