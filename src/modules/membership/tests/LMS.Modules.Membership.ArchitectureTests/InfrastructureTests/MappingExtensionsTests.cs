using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.InfrastructureTests;

public class MappingExtensionsTests : TestBase
{
    private const string MappingExtensionsNamespacePrefix = "LMS.Modules.Membership.Infrastructure.Mapping";

    [Fact]
    public void MappingExtensions_ShouldBe_NonPublicStaticClasses()
    {
        //Act
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceStartingWith(MappingExtensionsNamespacePrefix)
            .Should()
            .BeClasses()
            .And()
            .BeStatic()
            .And()
            .NotBePublic()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void MappingExtenions_ShouldEndWith_ExtensionsPostfix()
    {
        //Act
        TestResult result = Types
            .InAssembly(InfrastructureAssembly)
            .That()
            .ResideInNamespaceStartingWith(MappingExtensionsNamespacePrefix)
            .Should()
            .HaveNameEndingWith("Extensions")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
