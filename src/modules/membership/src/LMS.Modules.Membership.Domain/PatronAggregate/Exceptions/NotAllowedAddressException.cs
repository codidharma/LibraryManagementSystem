using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

public sealed class NotAllowedAddressException(string message) : LmsException(message);
