using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class DomainEventTests : TestBase
{
    [Fact]
    public void DomainEvents_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(DomainEvent))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);

    }

    [Fact]
    public void DomainEvents_ShouldEndWith_DomainEventPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(DomainEvent))
            .Should()
            .HaveNameEndingWith("DomainEvent")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);

    }
}
