using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LMS.Modules.IAM.Domain;

[ExcludeFromCodeCoverage]
internal static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
