namespace LMS.Common.Domain.Tests.ResultsTests;

public class ErrorTests
{
    [Fact]
    public void New_ShouldReturn_FailureErrorTypeInstance()
    {
        //Arrange
        string code = "Error.Failure";
        string description = "This is an error message.";
        ErrorType errorType = ErrorType.Failure;

        //Act
        Error error = new(code, description, errorType);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(errorType, error.ErrorType);
    }

    [Fact]
    public void Failure_ShouldReturn_FailureErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.Failure";
        string description = "This is an error message.";

        //Act
        Error error = Error.Failure(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(ErrorType.Failure, error.ErrorType);
    }

    [Fact]
    public void New_ShouldReturn_NotFoundErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.NotFound";
        string description = "Entity with id x was not found.";
        ErrorType errorType = ErrorType.NotFound;

        //Act
        Error error = new(code, description, errorType);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(errorType, error.ErrorType);
    }

    [Fact]
    public void NotFound_ShouldReturn_NotFoundErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.NotFound";
        string description = "Entity with id x was not found.";

        //Act
        Error error = Error.NotFound(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(ErrorType.NotFound, error.ErrorType);
    }

    [Fact]
    public void New_ShouldReturn_ConflictErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.Conflict";
        string description = "Entity with id x already exists.";
        ErrorType errorType = ErrorType.Conflict;

        //Act
        Error error = new(code, description, errorType);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(errorType, error.ErrorType);
    }

    [Fact]
    public void Conflict_ShouldReturn_ConflictErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.Conflict";
        string description = "Entity with id x already exists.";

        //Act
        Error error = Error.Conflict(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(ErrorType.Conflict, error.ErrorType);
    }

    [Fact]
    public void InvalidDomain_ShouldReturn_InvalidDomainErrorTypeInsatnce()
    {
        //Arrange
        string code = "Error.InvalidDomain";
        string description = "Entity with id x already exists.";

        //Act
        Error error = Error.InvalidDomain(code, description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void None_ShouldReturn_NoneErrorTypeInstance()
    {
        //Act
        Error error = Error.None;

        //Assert
        Assert.Empty(error.Code);
        Assert.Empty(error.Description);
        Assert.Equal(ErrorType.None, error.ErrorType);
    }

    [Fact]
    public void NullValue_ShouldRetuen_FailureErrorTypeInstance()
    {
        //Arrange
        string code = "Error.NullValue";
        string description = "Field x was null or empty.";
        //Act

        Error error = Error.NullValue(description);

        //Assert
        Assert.Equal(code, error.Code);
        Assert.Equal(description, error.Description);
        Assert.Equal(ErrorType.Failure, error.ErrorType);
    }

    [Fact]
    public void Validation_ShouldReturn_ValidationErrorTypeInstance()
    {
        //Arrange
        string code = "Generic.Validation";
        string description = "One or more validation error occured.";
        List<Error> errors =
        [
            Error.NullValue("Field x was null."),
            Error.NullValue("Field y was null."),
            Error.NullValue("Field z was null.")
        ];

        //Act
        ValidationError validationError = new(errors);

        //Assert
        Assert.Equal(code, validationError.Code);
        Assert.Equal(description, validationError.Description);
        Assert.Equal(ErrorType.Validation, validationError.ErrorType);
        Assert.Equal(errors, validationError.Errors.ToList());
    }

    [Fact]
    public void FromResult_ShouldReturn_ValidationErrorTypeInstance()
    {
        //Arrange
        string code = "Generic.Validation";
        string description = "One or more validation error occured.";
        List<Error> errors =
        [
            Error.NullValue("Field x was null."),
            Error.NullValue("Field y was null."),
            Error.NullValue("Field z was null."),
            Error.NullValue("Field x was null."),
            Error.NullValue("Field y was null."),
            Error.NullValue("Field z was null.")
        ];

        List<Result> results =
            [
            Result.Success(),
            Result.Failure(errors[0]),
            Result.Failure<DummyTestType>(errors[1]),
            Result.Failure(errors[2]),
            Result.Failure(errors[0]),
            Result.Failure<DummyTestType>(errors[1]),
            Result.Failure(errors[2]),
            Result.Success(new DummyTestType())
            ];


        //Act
        ValidationError validationError = ValidationError.FromResults(results);

        //Assert
        Assert.Equal(code, validationError.Code);
        Assert.Equal(description, validationError.Description);
        Assert.Equal(ErrorType.Validation, validationError.ErrorType);
        Assert.Equal(errors, validationError.Errors);
    }
}
