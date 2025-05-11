using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronCommandHandler : ICommandHandler<AddPatronCommand, Result<Response>>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<AddPatronCommand> _validator;
    private readonly ILogger _logger;

    public AddPatronCommandHandler(
        IPatronRepository patronRepository,
        IValidator<AddPatronCommand> validator,
        ILogger<AddPatronCommandHandler> logger,
        IUnitOfWork unitOfWork)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<Result<Response>> HandleAsync(AddPatronCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating the {Command}.", nameof(AddPatronCommand));

        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Finished validating the {Command} with status {Status}", nameof(AddPatronCommand), validationResult.IsValid);

        Result<Email> emailResult = Email.Create(command.Email);

        bool isPatronEmailAlreadyUsed = await _patronRepository.IsPatronEmailAlreadyUsedAsync(emailResult.Value, cancellationToken);

        if (isPatronEmailAlreadyUsed)
        {
            Error conflictError = Error.Conflict(ErrorCodes.Conflict, "The email provided is already taken.");
            Result<Response> conflictResult = Result.Failure<Response>(conflictError);
            return conflictResult;
        }

        List<Result> results = [];
        Result<Name> nameResult = Name.Create(command.Name);
        results.Add(nameResult);
        Result<Gender> genderResult = Gender.Create(command.Gender);
        results.Add(genderResult);
        Result<DateOfBirth> dobResult = DateOfBirth.Create(command.DateOfBirth);
        results.Add(dobResult);
        Result<PatronType> patronTypeResult = Enumeration.FromName<PatronType>(command.PatronType);
        results.Add(patronTypeResult);

        if (results.Any(r => r.IsFailure))
        {
            ValidationError validationError = ValidationError.FromResults(results);
            return Result.Failure<Response>(validationError);
        }

        Result<Patron> patronResult = Patron.Create(
            nameResult.Value,
            genderResult.Value,
            dobResult.Value,
            emailResult.Value,
            patronTypeResult.Value);

        _patronRepository.Add(patronResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Response response = new(patronResult.Value.Id.Value);

        Result<Response> idResult = Result.Success(response);

        return idResult;
    }
}
