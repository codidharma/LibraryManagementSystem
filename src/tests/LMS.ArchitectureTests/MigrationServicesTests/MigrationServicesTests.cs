using Microsoft.Extensions.Hosting;
using NetArchTest.Rules;

namespace LMS.ArchitectureTests.MigrationServicesTests;

public class MigrationServicesTests
{
    [Fact]
    public void WorkerServices_ShouldEndWith_WorkerPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(MigrationServices.AssemblyReference.Assembly)
            .That()
            .Inherit(typeof(BackgroundService))
            .Should()
            .HaveNameEndingWith("Worker")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
