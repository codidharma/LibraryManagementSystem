using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentContentTests : TestBase
{
    [Fact]
    public void New_ShouldThrow_InvalidValueException_ForInvalidBase64String()
    {
        //arrange
        string expectedExceptionMessage = $"The value provided is not a valid base64 string.";
        string sampleData = "somedata#!@!@!@";
        DocumentContent content;

        //Act
        Action action = () => { content = new(sampleData); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void New_ShouldReturn_ValidDocumentContent()
    {
        //Arrange
        string content = "somedata";

        //Act
        DocumentContent documentContent = new(content);

        //Assert
        Assert.Equal(content, documentContent.Value);
    }
}
