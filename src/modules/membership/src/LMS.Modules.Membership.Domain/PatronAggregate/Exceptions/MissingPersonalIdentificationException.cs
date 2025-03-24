using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

public sealed class MissingPersonalIdentificationException(string message) : LmsException(message);
