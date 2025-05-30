namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;

public sealed record CommandResponse(string Email, string TemporaryPassword);
