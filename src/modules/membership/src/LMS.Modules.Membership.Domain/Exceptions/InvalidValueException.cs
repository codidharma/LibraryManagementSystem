using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.Exceptions;

public sealed class InvalidValueException(string message) : LmsException(message);
