namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;

public record AddPatronCommand(
    string Name,
    string Gender,
    DateTime DateOfBirth,
    string Email,
    string NationalId,
    string PatronType);
