using FluentValidation;

namespace LMS.Modules.Membership.Application.Patron.OnboardRegularPatron;

public sealed class OnboardRegularPatronCommandValidator : AbstractValidator<OnboardRegularPatronCommand>
{
    public OnboardRegularPatronCommandValidator()
    {
        RuleFor(command => command.Name).NotEmpty();
        RuleFor(command => command.Gender).NotEmpty();
        RuleFor(command => command.Email).NotEmpty();
        RuleFor(command => command.Address).SetValidator(new AddressValidator());
        RuleForEach(command => command.IdentityDocuments).SetValidator(new DocumentValidator());

    }
}

public sealed class AddressValidator : AbstractValidator<Address>
{
    public AddressValidator()
    {
        RuleFor(address => address.StreetName).NotEmpty();
        RuleFor(address => address.City).NotEmpty();
        RuleFor(address => address.State).NotEmpty();
        RuleFor(address => address.Country).NotEmpty();
        RuleFor(address => address.ZipCode).NotEmpty();
    }
}

public sealed class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        RuleFor(doc => doc.DocumentType).NotEmpty();
        RuleFor(doc => doc.ContentType).NotEmpty();
        RuleFor(doc => doc.Content).NotEmpty();
    }

}
