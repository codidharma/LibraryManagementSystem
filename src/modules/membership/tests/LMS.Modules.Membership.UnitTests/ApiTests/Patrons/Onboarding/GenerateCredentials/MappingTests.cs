using LMS.Modules.Membership.Api.Patrons.Onboarding.GenerateCredentials;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.GenerateCredentials;

public class MappingTests
{
    [Fact]
    public void ToCommand_ShouldReturn_GenerateCredentialsCommandInstance()
    {
        //Arrange
        Request request = new(Guid.NewGuid());

        //Act
        GenerateCredentialsCommand command = request.ToCommand();

        //Assert
        Assert.Equal(request.PatronId, command.PatronId);

    }

    [Fact]
    public void ToDto_ShouldReturn_ResponseInstance()
    {
        //Arrange
        CommandResponse commandResponse = new("someemail@test.com", "somepassword");

        //Act
        Response response = commandResponse.ToDto();

        //Assert
        Assert.Equal(commandResponse.Email, response.Email);
        Assert.Equal(commandResponse.TemporaryPassword, response.TemporaryPassword);
    }
}
