using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Infrastructure.Data.Repositories;

internal sealed class PatronRepository : IPatronRepository
{
    private readonly MembershipDbContext _context;
    public PatronRepository(MembershipDbContext context)
    {
        _context = context;
    }
    public void Add(Patron patron)
    {
        _context.Patrons.Add(patron);
    }
}
