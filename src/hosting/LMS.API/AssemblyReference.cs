using System.Reflection;

namespace LMS.API;

internal static class AssemblyReference
{
    public static readonly Assembly HostingApiAssembly = typeof(AssemblyReference).Assembly;
}
