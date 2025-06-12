namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentsListByPatronId;

public sealed record QueryResponse(List<DocumentMetadata> Documents);

public sealed record DocumentMetadata(string Name, string DocumentType, string ContentType, Guid DocumentId);

