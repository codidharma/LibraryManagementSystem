using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;
using Microsoft.Extensions.Logging;
using AppDocument = LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments.Document;
using DomainDocument = LMS.Modules.Membership.Domain.PatronAggregate.Document;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;

public sealed class AddDocumentsCommandHandler : ICommandHandler<AddDocumentsCommand, Result>
{
    private readonly IValidator<AddDocumentsCommand> _validator;
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public AddDocumentsCommandHandler(
        IValidator<AddDocumentsCommand> validator,
        IPatronRepository patronRepository,
        IUnitOfWork unitOfWork,
        ILogger<AddDocumentsCommandHandler> logger)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result> HandleAsync(AddDocumentsCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting vaidation for command {Command}", nameof(AddDocumentsCommand));

        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            List<Error> errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage, ErrorType.Validation)).ToList();

            ValidationError validationError = new(errors);

            return Result.Failure(validationError);

        }
        _logger.LogInformation("Finished vaidation for command {Command} with status {Status}", nameof(AddDocumentsCommand), validationResult.IsValid);

        Patron? patron = await _patronRepository.GetByIdAsync(command.PatronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result notFoundResult = Result.Failure(error);
            return notFoundResult;
        }

        List<Result<DomainDocument>> domainDocumentResults = [];

        foreach (AppDocument document in command.Documents)
        {
            Result<Name> nameResult = Name.Create(document.Name);
            DocumentType documentType = Enumeration.FromName<DocumentType>(document.DocumentType).Value;
            DocumentContentType contentType = Enumeration.FromName<DocumentContentType>(document.ContentType).Value;
            Result<DocumentContent> contentResult = DocumentContent.Create(document.Content);

            Result<DomainDocument> documentResult = DomainDocument.Create(nameResult.Value, documentType,
                contentResult.Value, contentType);
            domainDocumentResults.Add(documentResult);
        }

        if (domainDocumentResults.Any(dr => dr.IsFailure))
        {
            ValidationError validationError = ValidationError.FromResults(domainDocumentResults);
            return Result.Failure(validationError);
        }

        foreach (Result<DomainDocument> domainDocumentResult in domainDocumentResults)
        {
            patron.AddDocument(domainDocumentResult.Value);
        }

        _patronRepository.Update(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
