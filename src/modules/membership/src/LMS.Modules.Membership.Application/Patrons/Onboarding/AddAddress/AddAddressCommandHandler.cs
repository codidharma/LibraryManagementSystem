using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

internal sealed class AddAddressCommandHandler : ICommandHandler<AddAddressCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    public AddAddressCommandHandler(IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));

    }
    public async Task<Result> HandleAsync(AddAddressCommand command, CancellationToken cancellationToken)
    {
        Patron? patron = await _patronRepository.GetByIdAsync(command.PatronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result notFoundResult = Result.Failure(error);
            return notFoundResult;
        }

        return Result.Success();


    }
}
