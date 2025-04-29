using LMS.Modules.Membership.Api.Patrons.Onboarding.AddPatron;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.ApiTests.Patrons.Onboarding.AddPatron;

public class MappingTests : TestBase
{
    [Fact]
    public void ToCommand_ShouldReturn_AddPatronCommand()
    {
        //Arrange
        string name = Faker.Person.FullName;
        string gender = Faker.Person.Gender.ToString();
        DateTime dateOfBirth = Faker.Person.DateOfBirth;
        string email = Faker.Person.Email;
        string patronType = "Regular";
        Request request = new(name, gender, dateOfBirth, email, patronType);

        //Act
        AddPatronCommand command = request.ToCommand();

        //Assert
        Assert.Equal(name, command.Name);
        Assert.Equal(gender, command.Gender);
        Assert.Equal(dateOfBirth, command.DateOfBirth);
        Assert.Equal(email, command.Email);
        Assert.Equal(patronType, command.PatronType);

    }
}
