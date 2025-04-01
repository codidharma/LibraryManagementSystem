using LMS.Modules.Membership.Application.Common.Identity;

namespace LMS.Modules.Membership.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    public Task<Guid> RegisterPatronAsync(string name, string email, CancellationToken cancellationToken)
    {
        return Task.FromResult(Guid.NewGuid());
    }
}
