using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class ValueObjectTests : TestBase
{
    [Fact]
    public void ValueObjects_ShouldBe_Sealed_And_ShouldNotBe_Public()
    {
        TestResult result = Types
            .InAssembly(MembershipAssembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

}
