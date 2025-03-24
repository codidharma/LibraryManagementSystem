using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

public sealed class InvalidValueException(string message) : LmsException(message);
