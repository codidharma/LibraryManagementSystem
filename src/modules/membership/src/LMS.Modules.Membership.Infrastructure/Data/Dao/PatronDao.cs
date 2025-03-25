namespace LMS.Modules.Membership.Infrastructure.Data.Dao;

internal sealed class PatronDao
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Gender { get; init; }
    public DateTime DateOfBirth { get; init; }
    public string Email { get; init; }
    public AddressDao Address { get; init; }
    public string PatronType { get; init; }
    public List<DocumentDao> IdentityDocuments { get; init; }

}
