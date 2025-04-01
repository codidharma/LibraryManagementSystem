using System.Reflection;

namespace LMS.MigrationServices;

internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
