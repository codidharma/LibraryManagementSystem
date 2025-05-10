namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTests
{
    [Fact]
    public void Create_ShouldReturn_PdfDocumentInstance()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType = DocumentType.PersonalIdentification;
        string sampleData = "This is sample text";
        DocumentContentType contentType = DocumentContentType.Pdf;
        Name name = Name.Create("IdentityCard.pdf").Value;



        DocumentContent content = DocumentContent.Create(sampleData).Value;

        //Act
        Document document = Document.Create(name, documentType, content, contentType).Value;

        //Assert
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(content, document.Content);
        Assert.IsType<EntityId>(document.Id);
    }

    [Fact]
    public void Create_ShouldReturn_SuccessResult()
    {
        //Arrange
        DocumentType documentType = DocumentType.PersonalIdentification;
        string sampleData = "This is sample text";
        DocumentContentType contentType = DocumentContentType.Pdf;
        Name name = Name.Create("IdentityCard.pdf").Value;

        DocumentContent content = DocumentContent.Create(sampleData).Value;

        //Act
        Result<Document> documentResult = Document.Create(name, documentType, content, contentType);

        //Assert
        Assert.True(documentResult.IsSuccess);
        Assert.False(documentResult.IsFailure);

        Document document = documentResult.Value;
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(content, document.Content);
        Assert.IsType<EntityId>(document.Id);
    }

}
