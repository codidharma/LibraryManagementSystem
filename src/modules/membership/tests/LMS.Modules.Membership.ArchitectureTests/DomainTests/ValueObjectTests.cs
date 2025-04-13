using System.Reflection;
using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class ValueObjectTests : TestBase
{
    [Fact]
    public void ValueObjects_ShouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(ValueObject))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);
    }

    [Fact]
    public void ValueObjects_ShouldNotHave_PublicConstructors()
    {
        //Act
        IEnumerable<Type> valueObjectTypes = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(ValueObject))
            .GetTypes();

        List<Type> failingTypes = [];

        foreach (Type valueObjectType in valueObjectTypes)
        {
            ConstructorInfo[] infos = valueObjectType.GetConstructors();
            if (infos.Any(c => c.IsConstructor && c.IsPublic))
            {
                failingTypes.Add(valueObjectType);
            }
        }

        //Assert
        Assert.Empty(failingTypes);
    }

}
