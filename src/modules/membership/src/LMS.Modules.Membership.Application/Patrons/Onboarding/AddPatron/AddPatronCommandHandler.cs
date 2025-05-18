using LMS.Common.Application.Data;
using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronCommandHandler : ICommandHandler<AddPatronCommand, Result<Response>>
{
    private readonly IPatronRepository _patronRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPatronCommandHandler(
        IPatronRepository patronRepository,
        IUnitOfWork unitOfWork)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }


    public async Task<Result<Response>> HandleAsync(AddPatronCommand command, CancellationToken cancellationToken)
    {

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
        Result<NationalId> nationalIdResult = NationalId.Create(command.NationalId);
        results.Add(nationalIdResult);
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
            nationalIdResult.Value,
            patronTypeResult.Value);

        _patronRepository.Add(patronResult.Value);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        Response response = new(patronResult.Value.Id.Value);

        Result<Response> idResult = Result.Success(response);

        return idResult;
    }
}
