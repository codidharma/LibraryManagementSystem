using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class ExceptionsTests : TestBase
{
    private const string ExceptionNamespace = "LMS.Modules.Membership.API.Common.Domain.Exceptions";

    [Fact]
    public void Exceptions_ShouldEndWith_ExceptionSuffix()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace(ExceptionNamespace)
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Exceptions_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ResideInNamespace(ExceptionNamespace)
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);

    }
}
