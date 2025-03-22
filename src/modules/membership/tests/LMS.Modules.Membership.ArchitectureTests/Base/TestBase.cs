using System.Reflection;

namespace LMS.Modules.Membership.ArchitectureTests.Base;

public class TestBase
{
    protected readonly Assembly DomainAssembly = Domain.AssemblyReference.Assembly;
    protected readonly Assembly ApiAssembly = API.AssemblyReference.Assembly;
}
