using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

public sealed class MissingAddressProofException(string message) : LmsException(message);
