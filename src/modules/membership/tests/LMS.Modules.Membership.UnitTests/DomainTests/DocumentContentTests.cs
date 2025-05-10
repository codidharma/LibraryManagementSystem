namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentContentTests : TestBase
{
    [Fact]
    public void Create_ShouldReturn_SuccessResult()
    {
        //Arrange
        string content = "somedata";

        //Act
        Result<DocumentContent> dcResult = DocumentContent.Create(content);

        //Assert
        Assert.True(dcResult.IsSuccess);
        Assert.False(dcResult.IsFailure);

        DocumentContent documentContent = dcResult.Value;

        Assert.Equal(content, documentContent.Value);
    }

    [Fact]
    public void ForInvalidBase64String_Create_ShouldReturn_FailureResult()
    {
        //arrange
        string expectedExceptionMessage = $"The value provided is not a valid base64 string.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        string sampleData = "somedata#!@!@!@";

        //Act
        Result<DocumentContent> dcResult = DocumentContent.Create(sampleData);

        //Assert
        Assert.True(dcResult.IsFailure);
        Assert.False(dcResult.IsSuccess);

        Error error = dcResult.Error;

        Assert.Equal(expectedExceptionMessage, error.Description);
        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);

    }
}
