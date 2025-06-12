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

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Patron patron)
    {
        _context.Patrons.Add(patron);
    }

    public async Task<Document?> GetDocumentByIdAsync(EntityId id, CancellationToken cancellationToken = default)
    {
        Document? document = await _context.Documents.SingleOrDefaultAsync(d => d.Id == id, cancellationToken);

        return document;
    }

    public async Task<Patron?> GetPatronByIdAsync(EntityId id, CancellationToken cancellationToken = default)
    {
        Patron? patron = await _context.Patrons.SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        return patron;
    }

    public async Task<bool> IsPatronEmailAlreadyUsedAsync(Email email, CancellationToken cancellationToken = default)
    {
        Patron? patron = await _context.Patrons.SingleOrDefaultAsync(p => p.Email == email, cancellationToken);

        return !(patron is null);
    }

    public void Update(Patron patron)
    {
        _context.Update<Patron>(patron);
    }
}
