

using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainPersistanceMappingTests;

public class AddressMappingTests : TestBase
{
    [Fact]
    public void ToDao_Returns_DataAccessObject()
    {
        //Arrange
        string street = Faker.Address.StreetName();
        string city = Faker.Address.City();
        string state = Faker.Address.State();
        string country = Faker.Address.Country();
        string zipCode = Faker.Address.ZipCode();

        Address address = Address.Create(street, city, state, country, zipCode);

        //Act
        AddressDao dao = address.ToDao();

        //Assert
        Assert.Equal(street, dao.Street);
        Assert.Equal(city, dao.City);
        Assert.Equal(state, dao.State);
        Assert.Equal(country, dao.Country);
        Assert.Equal(zipCode, dao.ZipCode);
    }

    [Fact]
    public void ToDomainModel_Returns_DomainObject()
    {
        //Arrange
        string street = Faker.Address.StreetName();
        string city = Faker.Address.City();
        string state = Faker.Address.State();
        string country = Faker.Address.Country();
        string zipCode = Faker.Address.ZipCode();

        AddressDao addressDao = new()
        {
            Street = street,
            City = city,
            State = state,
            Country = country,
            ZipCode = zipCode
        };

        //Act
        Address domainModel = addressDao.ToDomainModel();

        //Assert
        Assert.Equal(street, domainModel.Street);
        Assert.Equal(city, domainModel.City);
        Assert.Equal(state, domainModel.State);
        Assert.Equal(country, domainModel.Country);
        Assert.Equal(zipCode, domainModel.ZipCode);
    }
}
