using FluentValidation.TestHelper;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.ApplicationTests.Onboarding.AddAddress;

public class ValidatorTests : TestBase
{
    private readonly AddAddressCommandValidator _validator = new();

    [Fact]
    public void ForValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string buildingNumber = Faker.Address.BuildingNumber().ToString();
        string streetName = Faker.Address.StreetName().ToString();
        string city = Faker.Address.City().ToString();
        string state = Faker.Address.State().ToString();
        string country = Faker.Address.Country().ToString();
        string zipCode = Faker.Address.ZipCode().ToString();

        AddAddressCommand addAddressCommand = new(
            patronId,
            buildingNumber,
            streetName,
            city,
            state,
            country,
            zipCode);

        //Act
        TestValidationResult<AddAddressCommand> result = _validator.TestValidate(addAddressCommand);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ForInValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        AddAddressCommand addAddressCommand = new(
            Guid.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty);

        //Act
        TestValidationResult<AddAddressCommand> result = _validator.TestValidate(addAddressCommand);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(c => c.PatronId);
        result.ShouldHaveValidationErrorFor(c => c.BuildingNumber);
        result.ShouldHaveValidationErrorFor(c => c.StreetName);
        result.ShouldHaveValidationErrorFor(c => c.City);
        result.ShouldHaveValidationErrorFor(c => c.State);
        result.ShouldHaveValidationErrorFor(c => c.Country);
        result.ShouldHaveValidationErrorFor(c => c.ZipCode);

    }

}
