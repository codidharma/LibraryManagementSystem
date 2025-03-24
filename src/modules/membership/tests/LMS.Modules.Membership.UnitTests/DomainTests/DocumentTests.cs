namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTests
{
    [Fact]
    public void Create_ShouldReturn_PdfDocumentInstance()
    {
        //Arrange
        DocumentType documentType = DocumentType.PersonalIdentification;
        string sampleData = "This is sample text";
        DocumentContentType contentType = DocumentContentType.Pdf;



        DocumentContent content = new(sampleData, contentType);

        //Act
        Document document = Document.Create(documentType, content);

        //Assert
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(content, document.Content);
        Assert.IsType<Guid>(document.Id);
    }
}
