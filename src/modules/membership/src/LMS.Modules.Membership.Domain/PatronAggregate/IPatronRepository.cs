using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public interface IPatronRepository : IRepository<Patron>
{
    void Add(Patron patron);
    Task<Patron?> GetPatronByIdAsync(EntityId id, CancellationToken cancellationToken = default);
    Task<bool> IsPatronEmailAlreadyUsedAsync(Email email, CancellationToken cancellationToken = default);
    void Update(Patron patron);

    Task<Patron?> GetPatronWithDocumentsAsync(EntityId id, CancellationToken cancellationToken = default);
    Task<Document?> GetDocumentByIdAsync(EntityId id, CancellationToken cancellationToken = default);
}
