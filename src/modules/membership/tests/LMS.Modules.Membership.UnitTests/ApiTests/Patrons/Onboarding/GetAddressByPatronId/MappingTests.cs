using LMS.Modules.Membership.Api.Patrons.Common.GetAddressByPatronId;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetAddressByPatronId;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.GetAddressByPatronId;

public class MappingTests : TestBase
{

    [Fact]
    public void ToDto_ShouldReturn_Response()
    {
        //Arrange
        string buildingNumber = Faker.Address.BuildingNumber().ToString();
        string streetName = Faker.Address.City().ToString();
        string city = Faker.Address.City().ToString();
        string state = Faker.Address.State().ToString();
        string country = Faker.Address.Country().ToString();
        string zipCode = Faker.Address.ZipCode().ToString();

        GetAddressByPatronIdQueryResponse getAddressByPatronIdQueryResponse = new(
            buildingNumber,
            streetName,
            city,
            state,
            country,
            zipCode
            );

        //Act
        Response response = getAddressByPatronIdQueryResponse.ToDto();

        //Assert
        Assert.Equal(buildingNumber, response.BuildingNumber);
        Assert.Equal(streetName, response.StreetName);
        Assert.Equal(city, response.City);
        Assert.Equal(state, response.State);
        Assert.Equal(country, response.Country);
        Assert.Equal(zipCode, response.ZipCode);
    }
}
