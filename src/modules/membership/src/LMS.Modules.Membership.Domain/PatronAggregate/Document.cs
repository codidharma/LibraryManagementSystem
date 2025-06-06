using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Common;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Document : Entity
{
    public Name Name { get; }
    public DocumentType DocumentType { get; }
    public DocumentContent Content { get; }
    public DocumentContentType ContentType { get; }

    private Document() { }
    private Document(Name name, DocumentType documentType, DocumentContent content, DocumentContentType contentType)
    {
        Name = name;
        DocumentType = documentType;
        Content = content;
        ContentType = contentType;
    }

    public static Result<Document> Create(
        Name name,
        DocumentType documentType,
        DocumentContent content,
        DocumentContentType contentType
        )
    {
        Document document = new Document(name, documentType, content, contentType);
        return Result.Success(document);
    }
}
