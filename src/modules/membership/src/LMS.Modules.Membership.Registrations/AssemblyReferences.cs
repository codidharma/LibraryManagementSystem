using System.Reflection;

namespace LMS.Modules.Membership.Registrations;

public static class AssemblyReferences
{
    public static readonly Assembly DomainAssembly = Domain.AssemblyReference.Assembly;
    public static readonly Assembly ApplicationAssembly = Application.AssemblyReference.Assembly;
    public static readonly Assembly ApiAssembly = API.AssemblyReference.Assembly;
    public static readonly Assembly InfrastructureAssembly = Infrastructure.AssemblyReference.Assembly;
    public static readonly Assembly MembershipRegistrationAssembly = typeof(AssemblyReferences).Assembly;
}
