namespace LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;

public sealed record AddAddressCommand(
    Guid PatronId,
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode);
