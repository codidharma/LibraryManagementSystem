using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentContentTests : TestBase
{
    [Fact]
    public void New_ShouldThrow_InvalidValueException_ForInvalidBase64String()
    {
        //arrange
        string sampleData = "somedata#!@!@!@";
        DocumentContentType contentType = DocumentContentType.Pdf;

        DocumentContent content;

        //Act
        Action action = () => { content = new(sampleData, contentType); };

        //Assert
        Assert.Throws<InvalidValueException>(action);
    }

    [Fact]
    public void New_ShouldReturn_PdfDocument()
    {
        //Arrange
        DocumentContentType contentType = DocumentContentType.Pdf;
        string content = "somedata";

        //Act
        DocumentContent documentContent = new(content, contentType);

        //Assert
        Assert.Equal(contentType, documentContent.ContentType);
        Assert.Equal(content, documentContent.Value);
    }

    [Fact]
    public void New_ShouldReturn_JpegDocument()
    {
        //Arrange
        string data = "somedata";
        DocumentContentType documentContentType = DocumentContentType.Jpeg;

        DocumentContent documentContent = new(data, documentContentType);

        //Assert
        Assert.Equal(documentContentType, documentContent.ContentType);
        Assert.Equal(data, documentContent.Value);
    }

    [Fact]
    public void New_ShouldReturn_JpgDocument()
    {
        //Arrange
        string data = "somedata";
        DocumentContentType documentContentType = DocumentContentType.Jpg;

        DocumentContent documentContent = new(data, documentContentType);

        //Assert
        Assert.Equal(documentContentType, documentContent.ContentType);
        Assert.Equal(data, documentContent.Value);
    }
}
