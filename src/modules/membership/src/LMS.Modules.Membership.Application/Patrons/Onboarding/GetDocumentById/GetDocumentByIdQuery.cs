namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentById;

public sealed record GetDocumentByIdQuery(Guid PatronId, Guid DocumentId);
