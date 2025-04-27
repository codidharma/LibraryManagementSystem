namespace LMS.Modules.Membership.Application.Patrons.OnboardingPatron.AddPatron;

public record AddPatronCommand(string Name, string Gender, DateTime DateOfBirth, string Email, string PatronType);
