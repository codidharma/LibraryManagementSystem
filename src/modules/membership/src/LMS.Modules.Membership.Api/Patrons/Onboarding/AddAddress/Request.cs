namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddAddress;

internal sealed record Request(
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode);
