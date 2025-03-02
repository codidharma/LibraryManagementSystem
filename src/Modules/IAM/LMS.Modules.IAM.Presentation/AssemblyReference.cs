using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace LMS.Modules.IAM.Presentation;

[SuppressMessage("Maintainability", "CA1515:Consider making public types internal", Justification = "Used to get assembly information in the test")]
public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;

}
