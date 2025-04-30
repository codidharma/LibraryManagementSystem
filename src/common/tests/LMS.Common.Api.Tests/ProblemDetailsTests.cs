using LMS.Common.Api.Results;
using LMS.Common.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
namespace LMS.Common.Api.Tests;

public class ProblemDetailsTests
{
    [Fact]
    public void ForValidationError_Create_ShouldReturn_ProblemDetailsInsatnce()
    {
        //Arrange
        string code = "Generic.Validation";
        string description = "One or more validation error occured.";
        int statusCode = StatusCodes.Status400BadRequest;
        string type = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
        List<Error> errors =
            [
                Error.InvalidDomain("Test.InvalidDomainValue", "Value provided was incorrect.")
            ];
        Dictionary<string, object?> extensions = [];
        extensions.Add("errors", errors);

        ValidationError validationError = new(errors);
        Result validationErrorResult = Result.Failure(validationError);

        //Act
        ProblemHttpResult problemDetailsHttpResult = (ProblemHttpResult)ProblemFactory.Create(validationErrorResult);
        ProblemDetails problemDetails = problemDetailsHttpResult.ProblemDetails;

        //Assert
        Assert.NotNull(problemDetailsHttpResult);
        Assert.NotNull(problemDetails);
        Assert.Equal(code, problemDetails.Title);
        Assert.Equal(description, problemDetails.Detail);
        Assert.Equal(statusCode, problemDetails.Status);
        Assert.Equal(type, problemDetails.Type);
        Assert.Equal(extensions, problemDetails.Extensions);
    }

    [Fact]
    public void ForUnspecifiedError_Create_ShouldReturn_ProblemDetailsInsatnce()
    {
        //Arrange
        string code = "Internal Server Error";
        string description = "An unexpected error occured. Please contact system administrator.";
        int statusCode = StatusCodes.Status500InternalServerError;
        string type = "https://tools.ietf.org/html/rfc7231#section-6.6.1";

        Error error = new Error(code, description, ErrorType.Failure);
        Result validationErrorResult = Result.Failure(error);

        //Act
        ProblemHttpResult problemDetailsHttpResult = (ProblemHttpResult)ProblemFactory.Create(validationErrorResult);
        ProblemDetails problemDetails = problemDetailsHttpResult.ProblemDetails;

        //Assert
        Assert.NotNull(problemDetailsHttpResult);
        Assert.NotNull(problemDetails);
        Assert.Equal(code, problemDetails.Title);
        Assert.Equal(description, problemDetails.Detail);
        Assert.Equal(statusCode, problemDetails.Status);
        Assert.Equal(type, problemDetails.Type);
        Assert.Empty(problemDetails.Extensions);
    }

    [Fact]
    public void ForNotFoundError_Create_ShouldReturn_ProblemDetailsInsatnce()
    {
        //Arrange
        string code = "Common.NotFound";
        string dummyId = Guid.NewGuid().ToString();
        string description = $"Unable to find the Common type instance for id {dummyId}";
        int statusCode = StatusCodes.Status404NotFound;
        string type = "https://tools.ietf.org/html/rfc7231#section-6.5.4";

        Error error = new Error(code, description, ErrorType.NotFound);
        Result validationErrorResult = Result.Failure(error);

        //Act
        ProblemHttpResult problemDetailsHttpResult = (ProblemHttpResult)ProblemFactory.Create(validationErrorResult);
        ProblemDetails problemDetails = problemDetailsHttpResult.ProblemDetails;

        //Assert
        Assert.NotNull(problemDetailsHttpResult);
        Assert.NotNull(problemDetails);
        Assert.Equal(code, problemDetails.Title);
        Assert.Equal(description, problemDetails.Detail);
        Assert.Equal(statusCode, problemDetails.Status);
        Assert.Equal(type, problemDetails.Type);
        Assert.Empty(problemDetails.Extensions);
    }

    [Fact]
    public void ForConflictError_Create_ShouldReturn_ProblemDetailsInsatnce()
    {
        //Arrange
        string code = "Common.Conflict";
        string dummyId = Guid.NewGuid().ToString();
        string description = $"Common type instance for id {dummyId} already exist";
        int statusCode = StatusCodes.Status409Conflict;
        string type = "https://tools.ietf.org/html/rfc7231#section-6.5.8";

        Error error = new Error(code, description, ErrorType.Conflict);
        Result validationErrorResult = Result.Failure(error);

        //Act
        ProblemHttpResult problemDetailsHttpResult = (ProblemHttpResult)ProblemFactory.Create(validationErrorResult);
        ProblemDetails problemDetails = problemDetailsHttpResult.ProblemDetails;

        //Assert
        Assert.NotNull(problemDetailsHttpResult);
        Assert.NotNull(problemDetails);
        Assert.Equal(code, problemDetails.Title);
        Assert.Equal(description, problemDetails.Detail);
        Assert.Equal(statusCode, problemDetails.Status);
        Assert.Equal(type, problemDetails.Type);
        Assert.Empty(problemDetails.Extensions);
    }

    [Fact]
    public void ForSuccessResult_Create_ShouldThrow_InvalidOperationException()
    {
        //Arrange
        string expectedExceptionMessage = $"This operation is not supported for {nameof(Result.Success)} result.";
        Result successResult = Result.Success();

        //Act
        IResult result;

        Action action = () => { result = ProblemFactory.Create(successResult); };

        //Assert
        InvalidOperationException ex = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(expectedExceptionMessage, ex.Message);
    }
}
