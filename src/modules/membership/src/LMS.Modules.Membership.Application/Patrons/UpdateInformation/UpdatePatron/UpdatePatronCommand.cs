namespace LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;

public sealed record UpdatePatronCommand(Guid PatronId, string Name, string Email);
