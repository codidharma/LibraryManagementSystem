
using Microsoft.AspNetCore.Diagnostics;

namespace LMS.ArchitectureTests.ApiTests;

public class ExceptionHandlerTests
{
    [Fact]
    public void ExceptionHandlers_ShouldBe_SealedNonPublicClass()
    {
        //Act
        TestResult result = Types.InAssembly(Api.AssemblyReference.HostingApiAssembly)
            .That()
            .ImplementInterface(typeof(IExceptionHandler))
            .Should()
            .NotBePublic()
            .And()
            .BeSealed()
            .And()
            .BeClasses()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ExceptionHandlers_ShouldEndWith_ExceptionHandlerPostfix()
    {
        //Act
        TestResult result = Types.InAssembly(Api.AssemblyReference.HostingApiAssembly)
            .That()
            .ImplementInterface(typeof(IExceptionHandler))
            .Should()
            .HaveNameEndingWith("ExceptionHandler")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
