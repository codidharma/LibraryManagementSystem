namespace LMS.Modules.Membership.Api.Patrons.Common.GetAddressByPatronId;

internal sealed record Response(
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode
    );
