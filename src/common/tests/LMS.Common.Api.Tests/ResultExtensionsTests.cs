using LMS.Common.Api.Results;
using LMS.Common.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using HttpResults = Microsoft.AspNetCore.Http.Results;

namespace LMS.Common.Api.Tests;

public class ResultExtensionsTests
{
    [Fact]
    public void ForGenericSuccessResult_Match_ShouldReturn_PositiveResult()
    {
        //Arrange
        Result<DummyTestType> result = Result.Success(new DummyTestType());
        //Act
        IResult outResult = result.Match(HttpResults.Ok, ProblemFactory.Create);

        //Assert
        Assert.IsType<Ok<DummyTestType>>(outResult);
    }

    [Fact]
    public void ForGenericFailureResult_Match_ShouldReturn_FailureResult()
    {
        //Arrange
        string code = "Test.NotFound";
        string description = "Not found.";
        Error notFoundError = Error.NotFound(code, description);
        Result<DummyTestType> result = Result.Failure<DummyTestType>(notFoundError);

        //Act
        IResult outResult = result.Match(HttpResults.Ok, ProblemFactory.Create);


        //Assert
        Assert.IsType<ProblemHttpResult>(outResult);
        ProblemDetails problemDetails = ((ProblemHttpResult)outResult).ProblemDetails;
        Assert.Equal(code, problemDetails.Title);
        Assert.Equal(description, problemDetails.Detail);
        Assert.Equal(StatusCodes.Status404NotFound, problemDetails.Status);
        Assert.Empty(problemDetails.Extensions);
    }

    [Fact]
    public void ForSuccessResult_Match_ShouldReturn_PositiveResult()
    {
        //Arrange
        Result result = Result.Success();
        //Act
        IResult outResult = result.Match(HttpResults.NoContent, ProblemFactory.Create);

        //Assert
        Assert.IsType<NoContent>(outResult);
    }
}
