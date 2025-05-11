using FluentValidation;
using FluentValidation.Results;
using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;
using Microsoft.Extensions.Logging;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

internal sealed class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IValidator<AddAddressCommand> _validator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;
    public AddAddressCommandHandler(
        IPatronRepository patronRepository,
        IValidator<AddAddressCommand> validator,
        IUnitOfWork unitOfWork,
        ILogger<AddAddressCommandHandler> logger)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    }
    public async Task<Result> HandleAsync(AddAddressCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Validating the {Command}.", nameof(AddPatronCommand));

        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);

        _logger.LogInformation("Finished validating the {Command} with status {Status}", nameof(AddPatronCommand), validationResult.IsValid);

        if (!validationResult.IsValid)
        {
            List<Error> errors = validationResult.Errors.Select(e => new Error(e.ErrorCode, e.ErrorMessage, ErrorType.Validation)).ToList();

            ValidationError validationError = new(errors);

            return Result.Failure(validationError);
        }

        Patron? patron = await _patronRepository.GetByIdAsync(command.PatronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result notFoundResult = Result.Failure(error);
            return notFoundResult;
        }

        Result<Address> addressResult = Address
            .Create(command.BuildingNumber, command.StreetName, command.City, command.State, command.Country, command.ZipCode);

        if (addressResult.IsFailure)
        {
            ValidationError validationError = ValidationError.FromResults([addressResult]);
            return Result.Failure(validationError);
        }

        patron.AddAddress(addressResult.Value);
        _patronRepository.Update(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
