using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

public sealed class GenerateCredentialsCommandHandler : ICommandHandler<GenerateCredentialsCommand, Result<CommandResponse>>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public GenerateCredentialsCommandHandler(IPatronRepository patronRepository, IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    public async Task<Result<CommandResponse>> HandleAsync(GenerateCredentialsCommand command, CancellationToken cancellationToken)
    {
        EntityId patronId = new(command.PatronId);

        Patron? patron = await _patronRepository
            .GetPatronByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result<CommandResponse> notFoundResult = Result.Failure<CommandResponse>(error);
            return notFoundResult;
        }

        Guid accessId = await _identityService
            .RegisterPatronAsync(name: patron.Name.Value, email: patron.Email.Value, cancellationToken);

        Result setAccessIdResult = patron.SetAccessId(accessId);

        if (setAccessIdResult.IsFailure)
        {
            ValidationError validationError = ValidationError.FromResults([setAccessIdResult]);
            return Result.Failure<CommandResponse>(validationError);
        }

        _patronRepository.Update(patron);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        CommandResponse commandResponse = new(patron.Email.Value, "SomePassword");
        return Result.Success(commandResponse);
    }
}
