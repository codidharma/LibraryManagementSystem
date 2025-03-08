using LMS.Common.Domain;

namespace LMS.Modules.Membership.API.Common.Domain.Exceptions;

internal sealed class NotAllowedAddressException(string message) : LmsException(message);
