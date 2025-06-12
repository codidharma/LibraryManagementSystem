using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using LMS.Modules.Membership.Domain.Common;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

internal sealed class UpdatePatronCommandHandler : ICommandHandler<UpdatePatronCommand, Result>
{
    private readonly IPatronRepository _patronRepository;
    public UpdatePatronCommandHandler(IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
    }

    public async Task<Result> HandleAsync(UpdatePatronCommand command, CancellationToken cancellationToken)
    {
        EntityId patronId = new(command.PatronId);

        Patron? patron = await _patronRepository.GetPatronByIdAsync(patronId, cancellationToken);

        if (patron is null)
        {
            Error error = Error.NotFound(ErrorCodes.NotFound, $"The patron with id {command.PatronId} was not found.");
            Result notFoundResult = Result.Failure(error);
            return notFoundResult;
        }

        List<Result> results = [];
        Result<Name> nameResult = Name.Create(command.Name);
        results.Add(nameResult);
        Result<Email> emailResult = Email.Create(command.Email);
        results.Add(emailResult);

        if (results.Any(r => r.IsFailure))
        {
            ValidationError validationError = ValidationError.FromResults(results);
            return Result.Failure<CommandResult>(validationError);
        }

        bool isPatronEmailAlreadyUsed = await _patronRepository.IsPatronEmailAlreadyUsedAsync(emailResult.Value, cancellationToken);

        if (isPatronEmailAlreadyUsed)
        {
            Error conflictError = Error.Conflict(ErrorCodes.Conflict, "The email provided is already taken.");
            Result<CommandResult> conflictResult = Result.Failure<CommandResult>(conflictError);
            return conflictResult;
        }

        patron.UpdatePersonalInformation(name: nameResult.Value, email: emailResult.Value);
        _patronRepository.Update(patron);
        await _patronRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }
}
