using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.OnboardRegularPatron;

public sealed class OnboardRegularPatronCommandHandler : ICommandHandler<OnboardRegularPatronCommand, Guid>
{
    private readonly ILogger _logger;
    private readonly IValidator<OnboardRegularPatronCommand> _validator;
    private readonly IIdentityService _identityService;
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;


    public OnboardRegularPatronCommandHandler(
        ILogger<OnboardRegularPatronCommandHandler> logger,
        IValidator<OnboardRegularPatronCommand> validator,
        IIdentityService identityService,
        IPatronRepository patronRepository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _identityService = identityService;
        _patronRepository = patronRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Guid> HandleAsync(OnboardRegularPatronCommand command, CancellationToken cancellationToken)
    {
        //To Do
        // 1. Log the process -- done
        // 2. Validate the request -- done
        // 3. Create user in identity management
        // 4. Create Patron in the database
        // 5 Save Changes
        _logger.LogInformation("starting the process for onboarding regular patron");

        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
        {
            string errors = validationResult.ToString("|");
            _logger.LogError("Validation for {Command} failed with {Errors}", nameof(OnboardRegularPatronCommand), errors);

            throw new LmsException($"{nameof(OnboardRegularPatronCommand)} failed validation.");
        }

        Guid accessId = await _identityService.RegisterPatronAsync(command.Name, command.Email, cancellationToken);

        Result<Domain.PatronAggregate.Address> addressResult = Domain.PatronAggregate.Address.Create(
            buildingNumber: command.Address.BuildingNumber,
            street: command.Address.StreetName,
            city: command.Address.City,
            state: command.Address.State,
            country: command.Address.Country,
            zipCode: command.Address.ZipCode);


        List<Domain.PatronAggregate.Document> idenityDocuments = [];

        foreach (Document document in command.IdentityDocuments)
        {
            Result<DocumentContent> documentContentResult =
                DocumentContent.Create(document.Content);

            Domain.PatronAggregate.Document doc = Domain.PatronAggregate.Document.Create(
                name: Name.Create(document.Name).Value,
                documentType: Enumeration.FromName<DocumentType>(document.DocumentType).Value,
                content: documentContentResult.Value,
                contentType: Enumeration.FromName<DocumentContentType>(document.ContentType).Value).Value;

            idenityDocuments.Add(doc);
        }

        Result<Name> nameResult = Name.Create(command.Name);
        Result<Gender> genderResult = Gender.Create(command.Gender);
        Result<Email> emailResult = Email.Create(command.Email);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(command.DateOfBirth);
        Result<AccessId> accessIdResult = AccessId.Create(accessId);
        Patron patron = Patron.Create(
            name: nameResult.Value,
            gender: genderResult.Value,
            dateOfBirth: dobResult.Value,
            email: emailResult.Value,
            address: addressResult.Value,
            patronType: PatronType.Regular,
            identityDocuments: idenityDocuments,
            accessId: accessIdResult.Value);

        _patronRepository.Add(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Finished the process for onboarding regular patron");

        return patron.Id.Value;

    }
}
