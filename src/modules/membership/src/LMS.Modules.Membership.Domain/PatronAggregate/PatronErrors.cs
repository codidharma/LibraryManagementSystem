using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Common;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public static class PatronErrors
{
    public static Error PatronNotFound(Guid id)
        => new(ErrorCodes.NotFound, $"The patron with id {id.ToString()} was not found.", ErrorType.NotFound);

    public static Error EmailAlreadyTaken(string email)
        => new(ErrorCodes.Conflict, $"The email {email} is already taken.", ErrorType.Conflict);

    public static Error AddressNotFound(Guid id)
        => new(ErrorCodes.NotFound, $"There was no address found on the patron with id {id.ToString()}.", ErrorType.NotFound);

    public static Error DocumentNotFound(Guid id)
        => new(ErrorCodes.NotFound, $"The document with id {id.ToString()} was not found.", ErrorType.NotFound);
}
