namespace LMS.Modules.Membership.Application.Patrons.OnboardRegularPatron;

public record OnboardRegularPatronCommand(
    string Name,
    string Email,
    string Gender,
    DateTime DateOfBirth,
    Address Address,
    List<Document> IdentityDocuments);

public record Address(
    string BuildingNumber,
    string StreetName,
    string City,
    string State,
    string Country,
    string ZipCode);

public record Document(
    string Name,
    string DocumentType,
    string ContentType,
    string Content);
