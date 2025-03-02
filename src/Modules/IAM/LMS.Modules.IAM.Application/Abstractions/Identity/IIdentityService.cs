namespace LMS.Modules.IAM.Application.Abstractions.Identity;

public interface IIdentityService
{
    Task<Guid> RegisterUserAsync(IdentityUser identityUser, CancellationToken cancellationToken = default);
}
