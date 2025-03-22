using System.Reflection;
using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class EntityTests : TestBase
{
    [Fact]
    public void Entites_ShouldBe_Sealed()
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
            ConstructorInfo[] info = entityType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            if (info.Any())
            {
                failingTypes.Add(entityType);
            }
        }

        Assert.Empty(failingTypes);
    }

    [Fact]
    public void Entities_ShouldHave_ParameterlessPrivateConstructor()
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

            if (!infos.Any(x => x.IsPrivate && x.GetParameters().Length == 0))
            {
                failingTypes.Add(entityType);
            }
        }

        Assert.Empty(failingTypes);
    }
}
