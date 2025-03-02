using System.Reflection;
using LMS.Common.Domain;
using LMS.Modules.IAM.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.IAM.ArchitectureTests.Domain;

public class DomainTests : TestBase
{
    [Fact]
    public void Entities_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void Entities_ShouldHave_PrivateParemeterlessConstructor()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        List<Type> failingTypes = [];

        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] infos = entityType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);

            if (!infos.Any(c => c.IsPrivate && c.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void Entities_ShouldOnlyHave_PrivateConstructors()
    {
        IEnumerable<Type> entityTypes = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity))
            .GetTypes();

        List<Type> failingTypes = [];

        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] infos = entityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (infos.Any())
            {
                failingTypes.Add(entityType);
            }
        }

        Assert.Empty(failingTypes);
    }

    [Fact]

    public void CustomExceptions_ShouldEndWith_WordException()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Exception))
            .Should()
            .HaveNameEndingWith("Exception")
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void CustomExceptions_ShouldInherit_LmsException()
    {
        IEnumerable<Type> exceptionTypes = Types
            .InAssembly(DomainAssembly)
            .That()
            .HaveNameEndingWith("Exception")
            .GetTypes();

        List<Type> failingTypes = [];

        foreach (Type exceptionType in exceptionTypes)
        {
            if (exceptionType.BaseType != typeof(LmsException))
            {
                failingTypes.Add(exceptionType);
            }
        }

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void CustomExceptions_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(LmsException))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }
}
