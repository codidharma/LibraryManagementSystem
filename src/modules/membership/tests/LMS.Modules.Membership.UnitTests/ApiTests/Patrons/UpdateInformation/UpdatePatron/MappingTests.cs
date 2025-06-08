using LMS.Modules.Membership.Api.Patrons.UpdateInformation.UpdatePatron;
using LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.UpdateInformation.UpdatePatron;

public class MappingTests : TestBase
{
    [Fact]
    public void ToCommand_ShouldReturn_UpdatePatronCommandInstance()
    {
        //Arrange
        string name = Faker.Person.FullName;
        string email = Faker.Person.Email;
        Guid patronId = Guid.NewGuid();
        Request request = new(name, email);

        //Act
        UpdatePatronCommand command = request.ToCommand(patronId);

        //Assert
        Assert.Equal(patronId, command.PatronId);
        Assert.Equal(name, command.Name);
        Assert.Equal(email, command.Email);

    }
}
