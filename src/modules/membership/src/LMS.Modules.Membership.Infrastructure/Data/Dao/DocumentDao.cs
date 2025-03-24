namespace LMS.Modules.Membership.Infrastructure.Data.Dao;

public class DocumentDao
{
    public string Content { get; init; }
    public string ContentType { get; init; }
    public DocumentType DocumentType { get; init; }

}

public class DocumentType
{
    public string Name { get; init; }
    public int Id { get; init; }
}
