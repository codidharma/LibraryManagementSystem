namespace LMS.Modules.Membership.Infrastructure.Data.Dao;

public class AddressDao
{
    public Guid Id { get; init; }
    public string Street { get; init; }
    public string City { get; init; }
    public string State { get; init; }
    public string Country { get; init; }
    public string ZipCode { get; init; }
}
