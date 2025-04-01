using LMS.Common.Api;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.ApiTests;

public class ApiTests : TestBase
{
    [Fact]
    public void ApiEndpoints_ShouldBe_SealedNonPublicClasses()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .BeClasses()
            .And()
            .BeSealed()
            .And()
            .NotBePublic()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ApiEndpoints_ShouldHaveNameEndingWith_EndpointPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApiAssembly)
            .That()
            .ImplementInterface(typeof(IEndpoint))
            .Should()
            .HaveNameEndingWith("Endpoint")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

}
