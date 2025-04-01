using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;

namespace LMS.Modules.Membership.Infrastructure.Mappings.PatronAggregate;

internal static class DocumentExtensions
{
    public static DocumentDao ToDao(this Document document)
    {
        return new DocumentDao
        {
            Id = document.Id.Value,
            Content = document.Content.Value,
            ContentType = document.ContentType.Name,
            DocumentType = document.DocumentType.Name
        };
    }

    public static Document ToDomainModel(this DocumentDao dao)
    {
        Domain.PatronAggregate.DocumentType documentType = Domain.PatronAggregate.
            DocumentType.FromName<Domain.PatronAggregate.DocumentType>(dao.DocumentType);

        DocumentContentType documentContentType = DocumentContentType.FromName<DocumentContentType>(dao.ContentType);
        DocumentContent documentContent = new(dao.Content);
        Document document = Document.Create(documentType, documentContent, documentContentType);
        document.SetEntityId(dao.Id);
        return document;
    }

}
