using System.Reflection;

namespace LMS.Modules.IAM.ArchitectureTests.Base;
public abstract class TestBase
{
    protected static readonly Assembly DomainAssembly = typeof(IAM.Domain.AssemblyReference).Assembly;
    protected static readonly Assembly ApplicationAssembly = typeof(IAM.Application.AssemblyReference).Assembly;
    protected static readonly Assembly InfrastructureAssembly = typeof(IAM.Infrastructure.AssemblyReference).Assembly;
    protected static readonly Assembly PresentationAssembly = typeof(IAM.Presentation.AssemblyReference).Assembly;
}
