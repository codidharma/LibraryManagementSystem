using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class DomainServicesTests : TestBase
{
    [Fact]
    public void DomainServices_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .HaveNameEndingWith("Service")
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);

    }
}
