using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Patron?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        EntityId patronId = new(id);
        Patron? patron = await _context.Patrons.SingleOrDefaultAsync(p => p.Id == patronId, cancellationToken);
        return patron;
    }
}
