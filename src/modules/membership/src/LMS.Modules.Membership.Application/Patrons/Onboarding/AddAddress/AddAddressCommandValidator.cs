using FluentValidation;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

public sealed class AddAddressCommandValidator : AbstractValidator<AddAddressCommand>
{
    public AddAddressCommandValidator()
    {
        RuleFor(c => c.PatronId).NotEmpty();
        RuleFor(c => c.BuildingNumber).NotEmpty();
        RuleFor(c => c.StreetName).NotEmpty();
        RuleFor(c => c.City).NotEmpty();
        RuleFor(c => c.State).NotEmpty();
        RuleFor(c => c.Country).NotEmpty();
        RuleFor(c => c.ZipCode).NotEmpty();
    }
}
