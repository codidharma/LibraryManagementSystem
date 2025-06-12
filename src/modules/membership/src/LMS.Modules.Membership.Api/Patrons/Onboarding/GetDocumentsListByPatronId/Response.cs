namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetDocumentsListByPatronId;

internal sealed record Response(List<DocumentInformation> DocumentInformations);

internal sealed record DocumentInformation(string Name, string DocumentLink, string Href, string Rel, string Method);
