using System.Reflection;
using LMS.Common.Domain;
using LMS.Modules.Membership.ArchitectureTests.Base;
using NetArchTest.Rules;

namespace LMS.Modules.Membership.ArchitectureTests.DomainTests;

public class TypedEnumerationTests : TestBase
{
    [Fact]
    public void StronglyTypedEnumerations_shouldBe_Sealed()
    {
        TestResult result = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Enumeration))
            .Should()
            .BeSealed()
            .GetResult();

        Assert.True(result.IsSuccessful);

    }

    [Fact]
    public void StronglyTypedEnumerations_ShouldOnlyHve_PrivateConstructors()
    {
        //Arrange
        IEnumerable<Type> enumerationTypes = Types
            .InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Enumeration))
            .GetTypes();

        List<Type> failingTypes = [];

        foreach (Type enumerationType in enumerationTypes)
        {
            ConstructorInfo[] infos = enumerationType.GetConstructors(BindingFlags.Instance | BindingFlags.Public);

            if (infos.Any())
            {
                failingTypes.Add(enumerationType);

            }
        }

        Assert.Empty(failingTypes);
    }

}
