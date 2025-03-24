﻿using Bogus;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class AddressValidationServiceTests : TestBase
{
    [Fact]
    public void ForUnAllowedZipCodes_Service_ShouldReturn_False()
    {
        //Arrange
        PatronId patronId = new(Guid.NewGuid());
        Address address = Address.Create(patronId,
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

    [Theory]
    [InlineData("412105")]
    [InlineData("411027")]
    public void ForAllowedZipCodes_Service_ShouldReturn_True(string zipCode)
    {
        //Arrange
        PatronId patronId = new(Guid.NewGuid());
        Address address = Address.Create(patronId,
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            zipCode);

        //Act
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        //Assert
        Assert.True(isAddressAllowed);
    }
}
