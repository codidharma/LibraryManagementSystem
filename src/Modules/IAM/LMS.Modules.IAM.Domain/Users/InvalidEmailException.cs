using LMS.Common.Domain;

namespace LMS.Modules.IAM.Domain.Users;

public sealed class InvalidEmailException(string message) : LmsException(message);
