using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.Exceptions;

public sealed class MissingAddressProofException(string message) : LmsException(message);
