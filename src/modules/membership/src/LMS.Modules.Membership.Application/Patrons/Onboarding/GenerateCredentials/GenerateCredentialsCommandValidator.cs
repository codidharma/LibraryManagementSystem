using FluentValidation;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

public sealed class GenerateCredentialsCommandValidator : AbstractValidator<GenerateCredentialsCommand>
{
    public GenerateCredentialsCommandValidator()
    {
        RuleFor(c => c.PatronId).NotEmpty();
    }
}
