namespace LMS.Modules.Membership.Api.Patrons.OnboardRegularPatron;

internal sealed record Request(
    string Name,
    string Email,
    string Gender,
    DateTime DateOfBirth,
    Address Address,
    List<Document> IdentityDocuments);

internal sealed record Address(
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode);


internal sealed record Document(
    string Name,
    string DocumentType,
    string ContentType,
    string Content);


