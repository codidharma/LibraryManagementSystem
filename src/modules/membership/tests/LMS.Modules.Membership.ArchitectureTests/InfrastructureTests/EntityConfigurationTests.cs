using LMS.Modules.Membership.ArchitectureTests.Base;
using Microsoft.EntityFrameworkCore;

namespace LMS.Modules.Membership.ArchitectureTests.InfrastructureTests;

public class EntityConfigurationTests : TestBase
{
    [Fact]
    public void EntityConfigurations_ShouldBe_NonPublicSealedClasses()
    {
        //Act
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .Inherit(typeof(IEntityTypeConfiguration<>))
            .Should()
            .BeClasses()
            .And()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void EntityConfigurations_ShouldHave_ConfigurationPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .Inherit(typeof(IEntityTypeConfiguration<>))
            .Should()
            .NotHaveNameEndingWith("Configuration")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
