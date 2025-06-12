namespace LMS.Modules.Membership.Api.Patrons.Common.GetPatronById;

internal sealed record Response(
    Guid Id,
    string Name,
    string Gender,
    DateTime DateOfBirth,
    string Email,
    string NationalId,
    string PatronType);
