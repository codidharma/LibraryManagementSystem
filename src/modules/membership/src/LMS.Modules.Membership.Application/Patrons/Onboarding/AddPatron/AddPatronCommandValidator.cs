using FluentValidation;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

public sealed class AddPatronCommandValidator : AbstractValidator<AddPatronCommand>
{
    public AddPatronCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Gender).NotEmpty();
        RuleFor(command => command.DateOfBirth).NotEmpty();
        RuleFor(command => command.Email).NotEmpty();
        RuleFor(command => command.PatronType).NotEmpty();
    }
}
