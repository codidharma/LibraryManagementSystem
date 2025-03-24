using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

public sealed class MissingAcademicsIdentificationException(string message) : LmsException(message);
