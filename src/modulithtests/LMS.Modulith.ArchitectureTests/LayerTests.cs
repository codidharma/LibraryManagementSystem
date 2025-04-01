using LMS.Modules.Membership.Registrations;
using NetArchTest.Rules;


namespace LMS.Modulith.ArchitectureTests;

public class LayerTests
{
    [Fact]
    public void HostingApi_ShouldNotReference_MembershipModules_Directly()
    {
        string?[] notAllowedAssemblies = [
            AssemblyReferences.DomainAssembly.GetName().Name,
            AssemblyReferences.ApiAssembly.GetName().Name,
            AssemblyReferences.ApplicationAssembly.GetName().Name,
            AssemblyReferences.InfrastructureAssembly.GetName().Name
            ];

        TestResult result = Types
            .InAssembly(Api.AssemblyReference.HostingApiAssembly)
            .ShouldNot()
            .HaveDependencyOnAll(notAllowedAssemblies)
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
