﻿using LMS.Common.Application.Handlers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Common;
using LMS.Modules.Membership.Domain.PatronAggregate;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronCommandHandler : ICommandHandler<AddPatronCommand, Result<CommandResult>>
{
    private readonly IPatronRepository _patronRepository;

    public AddPatronCommandHandler(IPatronRepository patronRepository)
    {
        _patronRepository = patronRepository ?? throw new ArgumentNullException(nameof(patronRepository));
    }


    public async Task<Result<CommandResult>> HandleAsync(AddPatronCommand command, CancellationToken cancellationToken)
    {

        Result<Email> emailResult = Email.Create(command.Email);

        bool isPatronEmailAlreadyUsed = await _patronRepository.IsPatronEmailAlreadyUsedAsync(emailResult.Value, cancellationToken);

        if (isPatronEmailAlreadyUsed)
        {
            return Result.Failure<CommandResult>(PatronErrors.EmailAlreadyTaken(emailResult.Value.Value));
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
            return Result.Failure<CommandResult>(validationError);
        }

        Result<Patron> patronResult = Patron.Create(
            nameResult.Value,
            genderResult.Value,
            dobResult.Value,
            emailResult.Value,
            nationalIdResult.Value,
            patronTypeResult.Value);

        _patronRepository.Add(patronResult.Value);
        await _patronRepository.UnitOfWork.SaveChangesAsync(cancellationToken);

        CommandResult response = new(patronResult.Value.Id.Value);

        Result<CommandResult> idResult = Result.Success(response);

        return idResult;
    }
}
