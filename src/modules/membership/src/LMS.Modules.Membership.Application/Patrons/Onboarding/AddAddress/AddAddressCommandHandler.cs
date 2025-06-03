using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

internal sealed class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;
    public AddAddressCommandHandler(
        IPatronRepository patronRepository,
        IUnitOfWork unitOfWork)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));


    }
    public async Task<Result> HandleAsync(AddAddressCommand command, CancellationToken cancellationToken)
    {
        EntityId patronId = new(command.PatronId);
        Patron? patron = await _patronRepository.GetPatronByIdAsync(patronId, cancellationToken);

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

        Result addAddressResult = patron.AddAddress(addressResult.Value);

        if (addAddressResult.IsFailure)
        {
            ValidationError validationError = ValidationError.FromResults([addAddressResult]);
            return Result.Failure(validationError);
        }

        _patronRepository.Update(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
