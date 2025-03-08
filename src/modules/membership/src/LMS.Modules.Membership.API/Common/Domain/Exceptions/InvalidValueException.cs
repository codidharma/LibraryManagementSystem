using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain.Exceptions;

internal sealed class InvalidValueException(string message) : LmsException(message);
