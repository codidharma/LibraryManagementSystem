
using LMS.Modules.Membership.Api.Patrons.Common.GetPatronById;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.GetPatronById;

public class MappingTest : TestBase
{
    [Fact]
    public void ToDto_ShouldReturn_Response()
    {
        //Arrange
        string name = Faker.Person.FullName;
        string gender = Faker.Person.Gender.ToString();
        DateTime dateOfBirth = Faker.Person.DateOfBirth;
        string email = Faker.Person.Email;
        string nationalId = "ABC123456D";
        string patronType = "Regular";

        GetPatronByIdQueryResponse queryResponse = new(
            Id: Guid.NewGuid(),
            Name: name,
            Gender: gender,
            DateOfBirth: dateOfBirth,
            Email: email,
            NationalId: nationalId,
            PatronType: patronType
            );

        //Act
        Response response = queryResponse.ToDto();

        //Assert
        Assert.Equal(name, response.Name);
        Assert.Equal(gender, response.Gender);
        Assert.Equal(dateOfBirth, response.DateOfBirth);
        Assert.Equal(email, response.Email);
        Assert.Equal(nationalId, response.NationalId);
        Assert.Equal(patronType, response.PatronType);
    }

}
