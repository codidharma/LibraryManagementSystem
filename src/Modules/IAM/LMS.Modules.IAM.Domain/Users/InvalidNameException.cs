using LMS.Common.Domain;

namespace LMS.Modules.IAM.Domain.Users;

public sealed class InvalidNameException(string message) : LmsException(message);
