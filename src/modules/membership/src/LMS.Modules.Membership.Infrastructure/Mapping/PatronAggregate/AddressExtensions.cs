using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Infrastructure.Data.Dao;

namespace LMS.Modules.Membership.Infrastructure.Mapping.PatronAggregate;

public static class AddressExtensions
{
    public static AddressDao ToDao(this Address address)
    {
        return new AddressDao
        {
            Id = address.Id.Value,
            Street = address.Street,
            City = address.City,
            State = address.State,
            Country = address.Country,
            ZipCode = address.ZipCode
        };
    }

    public static Address ToDomainModel(this AddressDao dao)
    {
        Address address = Address.Create(dao.Street, dao.City, dao.State, dao.Country, dao.ZipCode);
        address.SetEntityId(dao.Id);
        return address;
    }
}
