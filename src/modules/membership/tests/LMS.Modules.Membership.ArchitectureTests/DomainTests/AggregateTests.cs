using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class AggregateTests : TestBase
{
    [Fact]
    public void Aggregates_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .ImplementInterface(typeof(IAggregateRoot))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
