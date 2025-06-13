using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

internal sealed class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    public AddAddressCommandHandler(
        IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));

    }
    public async Task<Result> HandleAsync(AddAddressCommand command, CancellationToken cancellationToken)
    {
        EntityId patronId = new(command.PatronId);
        Patron? patron = await _patronRepository.GetPatronByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            return Result.Failure(PatronErrors.PatronNotFound(command.PatronId));
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
        await _patronRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
