using FluentValidation;

namespace LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

public sealed class UpdatePatronCommandValidator : AbstractValidator<UpdatePatronCommand>
{
    public UpdatePatronCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Email).NotEmpty();
    }
}
