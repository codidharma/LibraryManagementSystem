namespace LMS.Modules.IAM.Application.Users.RegisterUser;

public sealed record RegisterUserRequest(string FirstName, string LastName, string Email, string Password, UserRole role);
