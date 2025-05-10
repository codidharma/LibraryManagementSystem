namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;

public sealed record AddDocumentsCommand(Guid PatronId, IReadOnlyCollection<Document> Documents);

public sealed record Document(string Name, string DocumentType, string ContentType, string Content);

