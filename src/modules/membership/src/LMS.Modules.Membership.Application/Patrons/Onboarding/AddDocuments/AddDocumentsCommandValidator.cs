using FluentValidation;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;

internal sealed class AddDocumentsCommandValidator : AbstractValidator<AddDocumentsCommand>
{
    public AddDocumentsCommandValidator()
    {
        RuleFor(c => c.PatronId).NotEmpty();
        RuleFor(c => c.Documents).NotEmpty();
        RuleForEach(c => c.Documents).SetValidator(new DocumentValidator());
    }
}

internal sealed class DocumentValidator : AbstractValidator<Document>
{
    public DocumentValidator()
    {
        RuleFor(doc => doc.Name).NotEmpty();
        RuleFor(doc => doc.DocumentType).NotEmpty();
        RuleFor(doc => doc.ContentType).NotEmpty();
        RuleFor(doc => doc.Content).NotEmpty();
    }
}
