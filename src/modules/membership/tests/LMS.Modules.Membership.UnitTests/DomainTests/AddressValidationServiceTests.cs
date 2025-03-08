using Bogus;
using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class AddressValidationServiceTests : TestBase
{
    [Fact]
    public void ForUnAllowedZipCodes_Service_ShouldReturn_False()
    {
        //Arrange
        Address address = new(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode());

        //Act
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        //Assert
        Assert.False(isAddressAllowed);
    }
}
