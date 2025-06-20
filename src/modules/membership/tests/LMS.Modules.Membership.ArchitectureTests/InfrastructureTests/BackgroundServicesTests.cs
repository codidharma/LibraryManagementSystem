using LMS.Modules.Membership.ArchitectureTests.Base;
using Microsoft.Extensions.Hosting;

namespace LMS.Modules.Membership.ArchitectureTests.InfrastructureTests;

public class BackgroundServicesTests : TestBase
{
    [Fact]
    public void BackgroundServices_ShouldBe_SealedAndNonPublicClasses()
    {
        //Arrange
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .Inherit(typeof(BackgroundService))
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
    public void BackgroundServices_ShouldHave_ServicePostfix()
    {
        //Arrange
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .Inherit(typeof(BackgroundService))
            .Should()
            .HaveNameEndingWith("Service")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
