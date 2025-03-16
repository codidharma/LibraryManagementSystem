using LMS.Modules.Membership.API.Common.Domain;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class DocumentTests
{
    [Fact]
    public void Create_ShouldReturn_PdfDocumentInstance()
    {
        //Arrange
        DocumentType documentType = DocumentType.Pdf;
        string sampleData = "This is sample text";



        DocumentContent content = new(sampleData);

        //Act
        Document document = Document.Create(documentType, content);

        //Assert
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(content, document.Content);
    }


}
