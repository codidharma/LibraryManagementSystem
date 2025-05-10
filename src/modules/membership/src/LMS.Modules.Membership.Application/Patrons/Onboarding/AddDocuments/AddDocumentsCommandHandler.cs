using LMS.Common.Application.Handlers;
using LMS.Common.Domain;

namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;

public sealed class AddDocumentsCommandHandler : ICommandHandler<AddDocumentsCommand, Result>
{
    public Task<Result> HandleAsync(AddDocumentsCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
