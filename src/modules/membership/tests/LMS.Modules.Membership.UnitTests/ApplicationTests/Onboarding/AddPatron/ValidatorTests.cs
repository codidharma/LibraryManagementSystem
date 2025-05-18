using FluentValidation.TestHelper;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

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
        string nationalId = "AB123456D";
        string patronType = "Regular";
        AddPatronCommand addPatronCommand = new(name, gender, dateOfBirth, email, nationalId, patronType);

        //Act
        TestValidationResult<AddPatronCommand> result = _validator.TestValidate(addPatronCommand);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("", "", "", "", "")]
    public void ForInvalidCommand_Validator_ShouldReturn_IsValidAsFalse(
        string name,
        string gender,
        string email,
        string nationalId,
        string patronType
        )
    {
        //Arrange
        DateTime dateOfBirth = DateTime.Now;

        //Act
        AddPatronCommand command = new AddPatronCommand(name, gender, dateOfBirth, email, nationalId, patronType);
        TestValidationResult<AddPatronCommand> result = _validator.TestValidate(command);

        //Arrange
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(command => command.Name);
        result.ShouldHaveValidationErrorFor(command => command.Gender);
        result.ShouldHaveValidationErrorFor(command => command.Email);
        result.ShouldHaveValidationErrorFor(command => command.PatronType);
        result.ShouldNotHaveValidationErrorFor(command => command.DateOfBirth);
        result.ShouldHaveValidationErrorFor(command => command.NationalId);
    }
}
