using LMS.Modules.Membership.UnitTests.Base;
using LMS.Modules.Membership.UnitTests.Helpers;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class AddressTests : TestBase
{
    [Fact]
    public void Create_ShouldReturn_Address()
    {
        //Arrange
        string buildingNumber = Faker.Address.BuildingNumber();
        string street = Faker.Address.StreetName();
        string city = Faker.Address.City();
        string state = Faker.Address.State();
        string country = Faker.Address.Country();
        string zipCode = Faker.Address.ZipCode();

        //Act
        Address address = Address.Create(buildingNumber, street, city, state, country, zipCode);

        //Assert
        Assert.Equal(street, address.Street);
        Assert.Equal(city, address.City);
        Assert.Equal(state, address.State);
        Assert.Equal(country, address.Country);
        Assert.Equal(zipCode, address.ZipCode);
        Assert.IsType<EntityId>(address.Id);
    }

    [Theory]
    [ClassData(typeof(InvalidAddressTestData))]
    public void ForInvalidParameters_Constructor_ShouldThrow_InvalidValueException(string buildingNumber,
        string street,
        string city,
        string state,
        string country,
        string zipCode)
    {
        //Arrange
        string expectdExceptionMessage = $"Address should be composed of non null, empty of whitespace values for {nameof(Address.BuildingNumber)}, {nameof(Address.Street)}, {nameof(Address.City)}, {nameof(Address.State)} {nameof(Address.Country)} and {nameof(Address.ZipCode)}";
        Address address;

        //Act
        Action action = () => { address = Address.Create(buildingNumber, street, city, state, country, zipCode); };

        //Assert
        InvalidValueException exception = Assert.Throws<InvalidValueException>(action);
        Assert.Equal(expectdExceptionMessage, exception.Message);

    }
}
