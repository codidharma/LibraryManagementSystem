namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;

public sealed record GetPatronByIdQueryResponse(
    Guid Id,
    string Name,
    string Gender,
    DateTime DateOfBirth,
    string Email,
    string NationalId,
    string PatronType);
