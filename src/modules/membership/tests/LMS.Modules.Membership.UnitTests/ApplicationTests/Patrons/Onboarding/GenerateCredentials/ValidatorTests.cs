using FluentValidation.TestHelper;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

namespace LMS.Modules.Membership.UnitTests.ApplicationTests.Patrons.Onboarding.GenerateCredentials;

public class ValidatorTests
{
    private readonly GenerateCredentialsCommandValidator _validator = new();

    [Fact]
    public void ForValidCommand_Validator_ShouldReturn_IsValidAsTrue()
    {
        //Arrange
        var patronId = Guid.NewGuid();
        GenerateCredentialsCommand command = new(patronId);

        //Act
        TestValidationResult<GenerateCredentialsCommand> result = _validator.TestValidate(command);

        //Assert
        Assert.True(result.IsValid);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ForInValidCommand_Validator_ShouldReturn_IsValidAsFalse()
    {
        //Arrange
        Guid patronId = Guid.Empty;
        GenerateCredentialsCommand command = new(patronId);

        //Act
        TestValidationResult<GenerateCredentialsCommand> result = _validator.TestValidate(command);

        //Assert
        Assert.False(result.IsValid);
        result.ShouldHaveValidationErrorFor(r => r.PatronId);
    }
}
