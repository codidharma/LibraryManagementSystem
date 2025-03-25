using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainPersistanceMappingTests;

public class DocumentMappingTests : TestBase
{
    [Fact]
    public void ToDao_ShouldReturn_DataAccessObject()
    {
        //Arrange
        Domain.PatronAggregate.DocumentType documentType = Domain.PatronAggregate.DocumentType.PersonalIdentification;
        string sampleData = "This is sample text";
        DocumentContentType contentType = DocumentContentType.Pdf;
        DocumentContent content = new(sampleData, contentType);
        Document document = Document.Create(documentType, content);

        //Act
        DocumentDao dao = document.ToDao();

        //Assert
        Assert.Equal(document.Id.Value, dao.Id);
        Assert.Equal(content.Value, dao.Content);
        Assert.Equal(content.ContentType.ToString(), dao.ContentType);
        Assert.Equal(documentType.Name, dao.DocumentType);
    }

    [Fact]
    public void ToDomainModel_ShouldReturn_DomainModelObeject()
    {
        //Arrange
        string content = "This is sample text";
        string contentType = "application/pdf";
        string documentTypeName = "PersonalId";

        DocumentDao documentDao = new()
        {
            Id = Guid.NewGuid(),
            Content = content,
            ContentType = contentType,
            DocumentType = documentTypeName
        };

        //Act
        Document domainModel = documentDao.ToDomainModel();

        //Assert
        Assert.Equal(documentDao.Id, domainModel.Id.Value);
        Assert.Equal(content, domainModel.Content.Value);
        Assert.Equal(contentType, domainModel.Content.ContentType.Name);
        Assert.Equal(documentTypeName, domainModel.DocumentType.Name);

    }

}
