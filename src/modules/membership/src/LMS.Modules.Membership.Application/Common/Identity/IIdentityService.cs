namespace LMS.Modules.Membership.Application.Common.Identity;

public interface IIdentityService
{
    Task<Guid> RegisterPatronAsync(string name, string email, CancellationToken cancellationToken);
}
