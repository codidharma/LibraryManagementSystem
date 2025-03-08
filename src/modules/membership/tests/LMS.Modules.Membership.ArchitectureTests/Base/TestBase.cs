using System.Reflection;
using LMS.Modules.Membership.API;

namespace LMS.Modules.Membership.ArchitectureTests.Base;

public class TestBase
{
    protected readonly Assembly MembershipAssembly = AssemblyReference.Assembly;
}
