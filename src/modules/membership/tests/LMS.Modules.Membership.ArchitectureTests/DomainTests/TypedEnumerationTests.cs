using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class TypedEnumerationTests : TestBase
{
    public void StronglyTypedEnumerations_shouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(MembershipAssembly)
            .That()
            .Inherit(typeof(Enumeration))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);

    }

}
