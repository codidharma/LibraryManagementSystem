using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Document : Entity
{
    public DocumentType DocumentType { get; }
    public DocumentContent Content { get; }
    public DocumentContentType ContentType { get; }

    private Document() { }
    private Document(DocumentType documentType, DocumentContent content, DocumentContentType contentType)
    {
        DocumentType = documentType;
        Content = content;
        ContentType = contentType;
    }

    public static Document Create(DocumentType documentType, DocumentContent content, DocumentContentType contentType)
    {
        return new Document(documentType, content, contentType);
    }
}
