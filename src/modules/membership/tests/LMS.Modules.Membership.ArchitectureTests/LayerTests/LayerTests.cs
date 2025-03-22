using LMS.Modules.Membership.ArchitectureTests.Base;


namespace LMS.Modules.Membership.ArchitectureTests.LayerTests;

public class LayerTests : TestBase
{
    [Fact]
    public void Domain_ShouldNotReference_AnyLayer()
    {
        //Arrange
        string?[] notAllowedLayers = [ApiAssembly.GetName().Name];

        //Act
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(notAllowedLayers)
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
