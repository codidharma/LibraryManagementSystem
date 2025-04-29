using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronCommandHandler : ICommandHandler<AddPatronCommand, Guid>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPatronCommand> _validator;
    private readonly ILogger _logger;

    public AddPatronCommandHandler(IPatronRepository patronRepository, IValidator<AddPatronCommand> validator, ILogger<AddPatronCommandHandler> logger, IUnitOfWork unitOfWork)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<Guid> HandleAsync(AddPatronCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating the {Command}.", nameof(AddPatronCommand));

        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Finished validating the {Command} with status {Status}", nameof(AddPatronCommand), validationResult.IsValid);

        Result<Name> nameResult = Name.Create(command.Name);
        Result<Gender> genderResult = Gender.Create(command.Gender);
        Result<Email> emailResult = Email.Create(command.Email);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(command.DateOfBirth);
        PatronType patronType = Enumeration.FromName<PatronType>(command.PatronType);

        Result<Patron> patronResult = Patron.Create(
            nameResult.Value,
            genderResult.Value,
            dobResult.Value,
            emailResult.Value,
            patronType);

        _patronRepository.Add(patronResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return patronResult.Value.Id.Value;
    }
}
