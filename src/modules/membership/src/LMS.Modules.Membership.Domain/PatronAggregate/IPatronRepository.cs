namespace LMS.Modules.Membership.Domain.PatronAggregate;

public interface IPatronRepository
{
    void Add(Patron patron);
    Task<Patron?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
