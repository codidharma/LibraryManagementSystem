using LMS.Common.Application;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.ApplicationTests;

public class CommandHandlerTests : TestBase
{
    [Fact]
    public void CommandHandlers_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandHandlers_ShouldEndWith_CommandHandlerPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandHandler<,>))
            .Should()
            .HaveNameEndingWith("CommandHandler")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandDispatchers_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandDispatcher))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandDispatchers_ShouldEndWith_DispatcherPostFix()
    {
        //Act
        TestResult result = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .ImplementInterface(typeof(ICommandDispatcher))
            .Should()
            .HaveNameEndingWith("Dispatcher")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
