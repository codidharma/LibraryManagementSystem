namespace LMS.Modules.IAM.Application.Abstractions.Identity;

public sealed record IdentityUser(string FirstName, string LastName, string Email, string Password);
