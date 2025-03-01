using System.Reflection;

namespace LMS.Modules.IaM.ArchTests;

#pragma warning disable CA1515 // Consider making public types internal
public abstract class BaseTest
#pragma warning restore CA1515 // Consider making public types internal
{
    protected static readonly Assembly ApiAssembly = typeof(LMS.Modules.IaM.Api.AssemblyReference).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(LMS.Modules.IaM.Application.AssemblyReference).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(LMS.Modules.IaM.Infrastructure.AssemblyReference).Assembly;
    protected static readonly Assembly DomainAssembly = typeof(LMS.Modules.IaM.Domain.AssemblyReference).Assembly;
}
