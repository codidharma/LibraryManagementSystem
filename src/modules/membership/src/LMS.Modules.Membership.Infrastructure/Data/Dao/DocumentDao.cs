namespace LMS.Modules.Membership.Infrastructure.Data.Dao;

internal sealed class DocumentDao
{
    public Guid Id { get; init; }
    public string Content { get; init; }
    public string ContentType { get; init; }
    public DocumentTypeDao DocumentType { get; init; }

}

internal sealed class DocumentTypeDao
{
    public string Name { get; init; }
    public int Id { get; init; }
}
