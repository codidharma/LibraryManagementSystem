using Bogus;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class AddressValidationServiceTests : TestBase
{
    [Fact]
    public void ForUnAllowedZipCodes_Service_ShouldReturn_False()
    {
        //Arrange
        Address address = Address.Create(Faker.Address.BuildingNumber(),
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode()).Value;

        //Act
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        //Assert
        Assert.False(isAddressAllowed);
    }

    [Theory]
    [InlineData("412105")]
    [InlineData("411027")]
    public void ForAllowedZipCodes_Service_ShouldReturn_True(string zipCode)
    {
        //Arrange
        Address address = Address.Create(Faker.Address.BuildingNumber(),
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            zipCode).Value;

        //Act
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        //Assert
        Assert.True(isAddressAllowed);
    }
}
