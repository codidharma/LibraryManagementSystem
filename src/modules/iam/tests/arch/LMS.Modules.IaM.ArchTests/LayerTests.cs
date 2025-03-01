using NetArchTest.Rules;

namespace LMS.Modules.IaM.ArchTests;

public class LayerTests : BaseTest
{
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApplicationLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .ShouldNot().HaveDependencyOn(ApplicationAssembly.FullName)
            .GetResult();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .ShouldNot().HaveDependencyOn(InfrastructureAssembly.FullName)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        TestResult result = Types.InAssembly(DomainAssembly)
            .ShouldNot().HaveDependencyOn(ApiAssembly.FullName)
            .GetResult();
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot().HaveDependencyOn(InfrastructureAssembly.FullName)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void AppliationLayer_ShouldNotHaveDependencyOn_ApiLayer()
    {
        TestResult result = Types.InAssembly(ApplicationAssembly)
            .ShouldNot().HaveDependencyOn(ApiAssembly.FullName)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

}
