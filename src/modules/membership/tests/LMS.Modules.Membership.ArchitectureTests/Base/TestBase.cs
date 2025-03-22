using System.Reflection;
using LMS.Modules.Membership.Domain;

namespace LMS.Modules.Membership.ArchitectureTests.Base;

public class TestBase
{
    protected readonly Assembly DomainAssembly = AssemblyReference.Assembly;
}
