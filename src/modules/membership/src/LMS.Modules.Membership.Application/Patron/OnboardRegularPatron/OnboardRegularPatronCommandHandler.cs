using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patron.OnboardRegularPatron;

public sealed class OnboardRegularPatronCommandHandler : ICommandHandler<OnboardRegularPatronCommand, Guid>
{
    private readonly ILogger _logger;
    private readonly IValidator<OnboardRegularPatronCommand> _validator;
    private readonly IIdentityService _identityService;
    private readonly IPatronRepository _patronRepository;


    public OnboardRegularPatronCommandHandler(
        ILogger<OnboardRegularPatronCommandHandler> logger,
        IValidator<OnboardRegularPatronCommand> validator,
        IIdentityService identityService,
        IPatronRepository patronRepository)
    {
        _logger = logger;
        _validator = validator;
        _identityService = identityService;
        _patronRepository = patronRepository;
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

        Domain.PatronAggregate.Address address = Domain.PatronAggregate.Address.Create(
            street: command.Address.StreetName,
            city: command.Address.City,
            state: command.Address.State,
            country: command.Address.Country,
            zipCode: command.Address.ZipCode);

        List<Domain.PatronAggregate.Document> idenityDocuments = [];

        foreach (Document document in command.IdentityDocuments)
        {
            Domain.PatronAggregate.Document doc = Domain.PatronAggregate.Document.Create(
                documentType: Enumeration.FromName<DocumentType>(document.DocumentType),
                content: new DocumentContent(document.Content),
                contentType: Enumeration.FromName<DocumentContentType>(document.ContentType));

            idenityDocuments.Add(doc);
        }

        Domain.PatronAggregate.Patron patron = Domain.PatronAggregate.Patron.Create(
            name: new Name(command.Name),
            gender: new Gender(command.Gender),
            dateOfBirth: new DateOfBirth(command.DateOfBirth),
            email: new Email(command.Email),
            address: address,
            patronType: PatronType.Regular,
            identityDocuments: idenityDocuments,
            accessId: new AccessId(accessId));

        _patronRepository.Add(patron);

        _logger.LogInformation("Finished the process for onboarding regular patron");
        return patron.Id.Value;

    }
}
