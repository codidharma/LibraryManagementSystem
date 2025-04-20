namespace LMS.Modules.Membership.UnitTests.Base;

public class PatronTestBase : TestBase
{
    private static readonly Document PersonalIdentification = Document.Create(
        Name.Create("IdentityCard.pdf").Value,
        DocumentType.PersonalIdentification,
        DocumentContent.Create("somedata").Value, DocumentContentType.Pdf);
    private static readonly Document AcademicsIdentification = Document.Create(
        Name.Create("CollegeCard.pdf").Value,
        DocumentType.AcademicsIdentification,
        DocumentContent.Create("somedata").Value, DocumentContentType.Pdf);
    private static readonly Document AddressProof = Document.Create(
        Name.Create("AddressProof.pdf").Value,
        DocumentType.AddressProof,
        DocumentContent.Create("somedata").Value, DocumentContentType.Pdf);

    protected readonly Name Name = Name.Create(Faker.Person.FullName).Value;
    protected readonly Gender Gender = Gender.Create(Faker.Person.Gender.ToString()).Value;
    protected readonly DateOfBirth DateOfBirth = DateOfBirth.Create(Faker.Person.DateOfBirth).Value;
    protected readonly Address Address = Address.Create(
        Faker.Address.BuildingNumber(),
        Faker.Address.StreetName(),
        Faker.Address.City(),
        Faker.Address.State(),
        Faker.Address.Country(),
        "412105").Value;
    protected readonly Email Email = Email.Create(Faker.Person.Email).Value;
    protected readonly AccessId AccessId = AccessId.Create(Guid.NewGuid()).Value;
    protected readonly PatronType RegularPatronType = PatronType.Regular;
    protected readonly PatronType ResearchPatronType = PatronType.Research;

    protected readonly Patron RegularPatron = Patron.Create(
        name: Name.Create(Faker.Person.FullName).Value,
        gender: Gender.Create(Faker.Person.Gender.ToString()).Value,
        dateOfBirth: DateOfBirth.Create(Faker.Person.DateOfBirth).Value,
        email: Email.Create(Faker.Person.Email).Value,
        patronType: PatronType.Regular).Value;

    protected readonly Patron ResearchPatron = Patron.Create(
        name: Name.Create(Faker.Person.FullName).Value,
        gender: Gender.Create(Faker.Person.Gender.ToString()).Value,
        dateOfBirth: DateOfBirth.Create(Faker.Person.DateOfBirth).Value,
        email: Email.Create(Faker.Person.Email).Value,
        patronType: PatronType.Research).Value;

    protected readonly List<Document> RegularPatronOnboardingDocuments = [PersonalIdentification, AddressProof];
    protected readonly List<Document> ResearchPatronOnboardingDocuments = [PersonalIdentification, AddressProof, AcademicsIdentification];
    protected readonly List<Document> PersonalIdentificationDocuments = [PersonalIdentification];
    protected readonly List<Document> ResearchPatronMissingAcademicsDocuments = [PersonalIdentification, AddressProof];
    protected readonly List<Document> ResearchPatronMissingAddressProofDocuments = [PersonalIdentification, AcademicsIdentification];
}
