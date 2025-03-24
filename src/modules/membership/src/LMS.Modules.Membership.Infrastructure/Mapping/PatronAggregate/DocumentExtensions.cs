using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;

namespace LMS.Modules.Membership.Infrastructure.Mapping.PatronAggregate;

public static class DocumentExtensions
{
    public static DocumentDao ToDao(this Document document)
    {
        return new DocumentDao
        {
            Content = document.Content.Value,
            ContentType = document.Content.ContentType.ToString(),
            DocumentType = new() { Name = document.DocumentType.Name, Id = document.DocumentType.Id }
        };
    }

    public static Document ToDomainModel(this DocumentDao dao)
    {
        Domain.PatronAggregate.DocumentType documentType = Domain.PatronAggregate.
            DocumentType.FromName<Domain.PatronAggregate.DocumentType>(dao.DocumentType.Name);

        DocumentContentType documentContentType = DocumentContentType.FromName<DocumentContentType>(dao.ContentType);
        DocumentContent documentContent = new(dao.Content, documentContentType);
        Document document = Document.Create(documentType, documentContent);
        return document;
    }
}
