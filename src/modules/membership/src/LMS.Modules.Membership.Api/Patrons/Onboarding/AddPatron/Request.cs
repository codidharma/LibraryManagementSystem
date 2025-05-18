namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddPatron;

internal sealed record Request(
    string Name,
    string Gender,
    DateTime DateOfBirth,
    string Email,
    string NationalId,
    string PatronType);
