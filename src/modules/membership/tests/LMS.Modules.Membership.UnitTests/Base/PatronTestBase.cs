namespace LMS.Modules.Membership.UnitTests.Base;

public class PatronTestBase : TestBase
{
    private static readonly Document PersonalIdentification = Document.Create(
    Domain.PatronAggregate.DocumentType.PersonalIdentification,
    new("somedata"), DocumentContentType.Pdf);
    private static readonly Document AcademicsIdentification = Document.Create(
        Domain.PatronAggregate.DocumentType.AcademicsIdentification,
        new("somedata"), DocumentContentType.Pdf);
    private static readonly Document AddressProof = Document.Create(
        Domain.PatronAggregate.DocumentType.AddressProof,
        new("somedata"), DocumentContentType.Pdf);

    protected readonly Name Name = Name.Create(Faker.Person.FullName).Value;
    protected readonly Gender Gender = Gender.Create(Faker.Person.Gender.ToString()).Value;
    protected readonly DateOfBirth DateOfBirth = new(Faker.Person.DateOfBirth);
    protected readonly Address Address = Address.Create(
        Faker.Address.BuildingNumber(),
        Faker.Address.StreetName(),
        Faker.Address.City(),
        Faker.Address.State(),
        Faker.Address.Country(),
        "412105").Value;
    protected readonly Email Email = Email.Create(Faker.Person.Email).Value;
    protected readonly AccessId AccessId = new(Guid.NewGuid());
    protected readonly PatronType RegularPatronType = PatronType.Regular;
    protected readonly PatronType ResearchPatronType = PatronType.Research;
    protected readonly List<Document> RegularPatronOnboardingDocuments = [PersonalIdentification, AddressProof];
    protected readonly List<Document> ResearchPatronOnboardingDocuments = [PersonalIdentification, AddressProof, AcademicsIdentification];
    protected readonly List<Document> PersonalIdentificationDocuments = [PersonalIdentification];
    protected readonly List<Document> ResearchPatronMissingAcademicsDocuments = [PersonalIdentification, AddressProof];
    protected readonly List<Document> ResearchPatronMissingAddressProofDocuments = [PersonalIdentification, AcademicsIdentification];
}
