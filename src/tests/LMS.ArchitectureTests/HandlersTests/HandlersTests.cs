using System.Reflection;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Application.Dispatchers.Query;
using LMS.Common.Application.Handlers;
using NetArchTest.Rules;

namespace LMS.ArchitectureTests.HandlersTests;

public class HandlersTests
{
    private readonly Assembly[] ApplicationAssemblies = [
        Common.Application.AssemblyReference.Assembly,
        Modules.Membership.Application.AssemblyReference.Assembly,
        ];

    [Fact]
    public void CommandHandlers_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
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
            .InAssemblies(ApplicationAssemblies)
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
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(ICommandDispatcher))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CommandDispatchers_ShouldEndWith_CommandDispatcherPostFix()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(ICommandDispatcher))
            .Should()
            .HaveNameEndingWith("CommandDispatcher")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryHandlers_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryHandlers_ShouldEndWith_QueryHandlerPostfix()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(IQueryHandler<,>))
            .Should()
            .HaveNameEndingWith("QueryHandler")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryDispatchers_ShouldBe_Sealed()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(IQueryDispatcher))
            .Should()
            .BeSealed()
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void QueryDispatchers_ShouldEndWith_QueryDispatcherPostFix()
    {
        //Act
        TestResult result = Types
            .InAssemblies(ApplicationAssemblies)
            .That()
            .ImplementInterface(typeof(IQueryDispatcher))
            .Should()
            .HaveNameEndingWith("QueryDispatcher")
            .GetResult();

        //Assert
        Assert.True(result.IsSuccessful);
    }
}
