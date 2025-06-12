using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentsListByPatronId;

public sealed class GetDocumentsListByPatronIdQueryHandler : IQueryHandler<Guid, Result<QueryResponse>>
{
    private readonly IPatronRepository _patronRepository;

    public GetDocumentsListByPatronIdQueryHandler(IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
    }
    public async Task<Result<QueryResponse>> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        EntityId patronId = new(id);

        Patron? patron = await _patronRepository.GetPatronWithDocumentsAsync(patronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound("Membership.NotFound", $"The patron with id {id.ToString()} was not found.");
            return Result.Failure<QueryResponse>(error);
        }

        List<DocumentMetadata> metadataCollection = [];

        foreach (Document document in patron.Documents)
        {
            DocumentMetadata documentMetadata = new(
                Name: document.Name.Value,
                DocumentType: document.DocumentType.Name,
                ContentType: document.ContentType.Name,
                DocumentId: document.Id.Value);

            metadataCollection.Add(documentMetadata);
        }
        QueryResponse queryResponse = new(metadataCollection);
        return Result.Success(queryResponse);
    }
}
