using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("LMS.Modules.IaM.ArchTests")]
namespace LMS.Modules.IaM.Api;

internal static class AssemblyReference
{
    public static readonly Assembly Assemby = typeof(AssemblyReference).Assembly;
}
