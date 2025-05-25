using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;
using AppDocument = LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments.Document;
using DomainDocument = LMS.Modules.Membership.Domain.PatronAggregate.Document;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;

public sealed class AddDocumentsCommandHandler : ICommandHandler<AddDocumentsCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AddDocumentsCommandHandler(
        IPatronRepository patronRepository,
        IUnitOfWork unitOfWork)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    }
    public async Task<Result> HandleAsync(AddDocumentsCommand command, CancellationToken cancellationToken)
    {
        Patron? patron = await _patronRepository.GetByIdAsync(command.PatronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result notFoundResult = Result.Failure(error);
            return notFoundResult;
        }

        List<Result> results = [];

        foreach (AppDocument document in command.Documents)
        {
            Result<Name> nameResult = Name.Create(document.Name);
            results.Add(nameResult);
            Result<DocumentType> documentTypeResult = Enumeration.FromName<DocumentType>(document.DocumentType);
            results.Add(documentTypeResult);
            Result<DocumentContentType> contentTypeResult = Enumeration.FromName<DocumentContentType>(document.ContentType);
            results.Add(contentTypeResult);
            Result<DocumentContent> contentResult = DocumentContent.Create(document.Content);
            results.Add(contentResult);

            if (documentTypeResult.IsFailure || contentTypeResult.IsFailure || contentResult.IsFailure)
            {
                continue;
            }

            Result<DomainDocument> documentResult = DomainDocument.Create(nameResult.Value, documentTypeResult.Value,
                contentResult.Value, contentTypeResult.Value);
            results.Add(documentResult);
        }

        if (results.Any(dr => dr.IsFailure))
        {
            ValidationError validationError = ValidationError.FromResults(results);
            return Result.Failure(validationError);
        }

        foreach (Result result in results)
        {
            if (result is Result<DomainDocument> domainDocumentResult)
            {
                patron.AddDocument(domainDocumentResult.Value);
            }
        }

        Result verifyDocumentsResult = patron.VerifyDocuments();

        if (verifyDocumentsResult.IsFailure)
        {
            ValidationError validationError = ValidationError.FromResults([verifyDocumentsResult]);
            return Result.Failure(validationError);
        }

        _patronRepository.Update(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
