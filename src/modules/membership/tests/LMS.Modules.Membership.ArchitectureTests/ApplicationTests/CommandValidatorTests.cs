using FluentValidation;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.ApplicationTests;

public class CommandValidatorTests : TestBase
{
    public void CommandValidators_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .Inherit(typeof(AbstractValidator<>))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
