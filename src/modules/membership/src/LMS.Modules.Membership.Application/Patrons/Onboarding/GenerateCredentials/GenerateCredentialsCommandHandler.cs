using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Common.Identity;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

public sealed class GenerateCredentialsCommandHandler : ICommandHandler<GenerateCredentialsCommand, Result<CommandResponse>>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IIdentityService _identityService;
    public GenerateCredentialsCommandHandler(IPatronRepository patronRepository, IIdentityService identityService)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
    }
    public async Task<Result<CommandResponse>> HandleAsync(GenerateCredentialsCommand command, CancellationToken cancellationToken)
    {
        EntityId patronId = new(command.PatronId);

        Patron? patron = await _patronRepository
            .GetPatronByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            return Result.Failure<CommandResponse>(PatronErrors.PatronNotFound(command.PatronId));
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
        await _patronRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        CommandResponse commandResponse = new(patron.Email.Value, "SomePassword");
        return Result.Success(commandResponse);
    }
}
