using FluentValidation.TestHelper;
using LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

namespace LMS.Modules.Membership.UnitTests.ApplicationTests.Patrons.UpdateInformation.UpdatePatron;

public class ValidatorTests : TestBase
{
    private readonly UpdatePatronCommandValidator _updatePatronCommandValidator = new UpdatePatronCommandValidator();

    [Fact]
    public void ForValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        Guid patronId = Guid.NewGuid();
        string name = Faker.Person.FullName;
        string email = Faker.Person.Email;
        UpdatePatronCommand command = new(patronId, name, email);

        //Act
        TestValidationResult<UpdatePatronCommand> result = _updatePatronCommandValidator.TestValidate(command);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData("", "")]
    public void ForInvalidCommand_Validator_ShouldReturn_IsValidAsFalse(string name, string email)
    {
        //Arrange
        Guid emptyPatronId = Guid.Empty;
        UpdatePatronCommand command = new(emptyPatronId, name, email);

        //Act
        TestValidationResult<UpdatePatronCommand> result = _updatePatronCommandValidator.TestValidate(command);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(r => r.Name);
        result.ShouldHaveValidationErrorFor(r => r.Email);
    }
}
