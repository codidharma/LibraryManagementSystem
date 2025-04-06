namespace LMS.Modules.Membership.Application.Patrons.OnboardRegularPatron;

public class OnboardRegularPatronCommand
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public List<Document> IdentityDocuments { get; set; }

}

public class Address
{
    public string BuildingNumber { get; set; }
    public string StreetName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string ZipCode { get; set; }
}

public class Document
{
    public string DocumentType { get; set; }
    public string ContentType { get; set; }
    public string Content { get; set; }
}
