using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.Exceptions;

public sealed class MissingPersonalIdentificationException(string message) : LmsException(message);
