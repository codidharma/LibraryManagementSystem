using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.Exceptions;

public sealed class MissingAcademicsIdentificationException(string message) : LmsException(message);
