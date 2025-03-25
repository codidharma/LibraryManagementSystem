using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.InfrastructureTests;

public class DaoTests : TestBase
{
    private const string DaoNamespacePrefix = "LMS.Modules.Membership.Infrastructure.Data.Dao";
    [Fact]
    public void Daos_ShouldBe_Sealed_And_NonPublic_Classes()
    {
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceStartingWith(DaoNamespacePrefix)
            .Should()
            .BeClasses()
            .And()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Daos_ShouldEndWith_DaoPostFix()
    {
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceStartingWith(DaoNamespacePrefix)
            .Should()
            .HaveNameEndingWith("Dao")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
