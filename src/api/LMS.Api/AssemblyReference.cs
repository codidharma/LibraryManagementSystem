using System.Reflection;

namespace LMS.Api;

internal static class AssemblyReference
{

    public static readonly Assembly HostingApiAssembly = typeof(AssemblyReference).Assembly;
}
