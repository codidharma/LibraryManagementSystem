using LMS.Modules.Membership.Api.Patrons.Onboarding.AddDocuments;
using Microsoft.AspNetCore.Http;
using Document = LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments.Document;


namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.AddDocuments;

public class MappingTests
{
    [Fact]
    public async Task ToDocumentAsync_ShouldReturn_DocumentInstance()
    {
        //Arrange
        string documentType = "IdentityDocument";
        string documentName = "Passport.pdf";
        string documentContent = "somedata";
        string contentType = "application/pdf";
        byte[] documentBytes = Convert.FromBase64String(documentContent);
        MemoryStream ms = new(documentBytes);
        FormFile formFile = new(
            baseStream: ms,
            baseStreamOffset: 0,
            length: ms.Length,
            name: documentType,
            fileName: documentName
            )
        {
            Headers = new HeaderDictionary(),
            ContentType = contentType
        };

        //Act
        Document document = await formFile.ToDocumentAsync();

        //Assert
        Assert.Equal(documentName, document.Name);
        Assert.Equal(documentType, document.DocumentType);
        Assert.Equal(documentContent, document.Content);
        Assert.Equal(contentType, document.ContentType);
    }
}
