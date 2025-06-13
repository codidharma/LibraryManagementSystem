using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentById;

public sealed class GetDocumentByIdQueryHandler : IQueryHandler<GetDocumentByIdQuery, Result<QueryResponse>>
{
    private readonly IPatronRepository _patronRespository;
    public GetDocumentByIdQueryHandler(IPatronRepository patronRepository)
    {
        _patronRespository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
    }
    public async Task<Result<QueryResponse>> HandleAsync(GetDocumentByIdQuery query, CancellationToken cancellationToken)
    {
        EntityId patronId = new(query.PatronId);

        Patron? patron = await _patronRespository.GetPatronByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            return Result.Failure<QueryResponse>(PatronErrors.PatronNotFound(query.PatronId));
        }


        EntityId documentId = new(query.DocumentId);

        Document? document = await _patronRespository.GetDocumentByIdAsync(documentId, cancellationToken);

        if (document is null)
        {
            return Result.Failure<QueryResponse>(PatronErrors.DocumentNotFound(query.DocumentId));
        }

        QueryResponse queryResponse = new(
            FileName: document.Name.Value,
            DocumentType: document.DocumentType.Name,
            Content: document.Content.Value
            );
        return Result.Success(queryResponse);
    }
}
