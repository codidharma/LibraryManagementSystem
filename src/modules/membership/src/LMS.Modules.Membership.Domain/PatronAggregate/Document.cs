using System.Diagnostics.CodeAnalysis;
using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Document : Entity
{
    public DocumentType DocumentType { get; }
    public DocumentContent Content { get; }

    [ExcludeFromCodeCoverage]
    private Document()
    { }
    private Document(DocumentType documentType, DocumentContent content)
    {
        DocumentType = documentType;
        Content = content;
    }

    public static Document Create(DocumentType documentType, DocumentContent content)
    {
        return new Document(documentType, content);
    }

}
