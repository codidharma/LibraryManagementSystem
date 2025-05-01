using LMS.Modules.Membership.Api.Patrons.Onboarding.AddAddress;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.AddAddress;

public class MappingTests : TestBase
{
    [Fact]
    public void ToCommand_ShouldReturn_AddAddressCommandInstance()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string buildingNumber = Faker.Address.BuildingNumber().ToString();
        string streetName = Faker.Address.StreetName().ToString();
        string city = Faker.Address.City().ToString();
        string state = Faker.Address.State().ToString();
        string country = Faker.Address.Country().ToString();
        string zipCode = Faker.Address.ZipCode().ToString();

        Request request = new(
            buildingNumber,
            streetName,
            city,
            state,
            country,
            zipCode
            );

        //Act
        AddAddressCommand command = request.ToCommand(patronId);

        //Assert
        Assert.Equal(patronId, command.PatronId);
        Assert.Equal(buildingNumber, command.BuildingNumber);
        Assert.Equal(streetName, command.StreetName);
        Assert.Equal(city, command.City);
        Assert.Equal(state, command.State);
        Assert.Equal(country, command.Country);
        Assert.Equal(zipCode, command.ZipCode);
    }

}
