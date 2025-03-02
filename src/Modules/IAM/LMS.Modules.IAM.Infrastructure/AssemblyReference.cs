using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LMS.Modules.IAM.Infrastructure;

[ExcludeFromCodeCoverage]
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
