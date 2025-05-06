namespace LMS.Modules.Membership.Application.Patrons.Onboarding.GetAddressByPatronId;

public sealed record GetAddressByPatronIdQueryResponse(
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode
    );
