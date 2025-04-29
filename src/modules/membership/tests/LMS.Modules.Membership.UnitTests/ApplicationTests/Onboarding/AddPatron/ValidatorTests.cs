using FluentValidation.Results;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.ApplicationTests.Onboarding.AddPatron;

public class ValidatorTests : TestBase
{
    private readonly AddPatronCommandValidator _validator = new();

    [Fact]
    public void ForValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        string name = Faker.Person.FullName;
        string gender = Faker.Person.Gender.ToString();
        DateTime dateOfBirth = Faker.Person.DateOfBirth;
        string email = Faker.Person.Email;
        string patronType = "Regular";
        AddPatronCommand addPatronCommand = new(name, gender, dateOfBirth, email, patronType);

        //Act
        ValidationResult result = _validator.Validate(addPatronCommand);

        //Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Theory]
    [InlineData("", "", "", "")]
    public void ForInvalidCommand_Validator_ShouldReturn_IsValidAsFalse(
        string name,
        string gender,
        string email,
        string patronType
        )
    {
        //Arrange
        DateTime dateOfBirth = DateTime.Now;

        //Act
        AddPatronCommand command = new AddPatronCommand(name, gender, dateOfBirth, email, patronType);
        ValidationResult result = _validator.Validate(command);

        //Arrange
        Assert.False(result.IsValid);
        Assert.Equal(4, result.Errors.Count);
    }
}
