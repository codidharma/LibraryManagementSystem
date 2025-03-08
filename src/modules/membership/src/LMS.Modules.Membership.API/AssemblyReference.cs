using System.Reflection;

namespace LMS.Modules.Membership.API;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
