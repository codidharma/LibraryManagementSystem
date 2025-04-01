namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTests
{
    [Fact]
    public void Create_ShouldReturn_PdfDocumentInstance()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType = Domain.PatronAggregate.DocumentType.PersonalIdentification;
        string sampleData = "This is sample text";
        DocumentContentType contentType = DocumentContentType.Pdf;



        DocumentContent content = new(sampleData);

        //Act
        Document document = Document.Create(documentType, content, contentType);

        //Assert
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(content, document.Content);
        Assert.IsType<EntityId>(document.Id);
    }
}
