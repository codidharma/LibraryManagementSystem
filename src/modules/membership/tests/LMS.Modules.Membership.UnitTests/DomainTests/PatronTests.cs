using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : TestBase
{
    private readonly Document PersonalIdentification = Document.Create(
        Domain.PatronAggregate.DocumentType.PersonalIdentification,
        new("somedata", DocumentContentType.Pdf));
    private readonly Document AcademicsIdentification = Document.Create(
        Domain.PatronAggregate.DocumentType.AcademicsIdentification,
        new("somedata", DocumentContentType.Pdf));
    private readonly Document AddressProof = Document.Create(
        Domain.PatronAggregate.DocumentType.AddressProof,
        new("somedata", DocumentContentType.Pdf));

    [Fact]
    public void Create_Should_ReturnRegularPatron()
    {
        //Arrange
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = Address.Create(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");
        Email email = new(Faker.Person.Email);

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [PersonalIdentification, AddressProof];


        //Act
        Patron regularPatron = Patron.Create(name, gender, dateOfBirth, email, address, patronType, onboardingDocuments);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(name, regularPatron.Name);
        Assert.Equal(dateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(address, regularPatron.Address);
        Assert.Equal(patronType, regularPatron.PatronType);
        Assert.Equal(email, regularPatron.Email);
        Assert.IsType<EntityId>(regularPatron.Id);
    }

    [Fact]
    public void Create_Should_ReturnReSearchPatron()
    {
        //Arrange
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");
        List<Document> onboardingDocuments = [PersonalIdentification, AcademicsIdentification, AddressProof];

        PatronType patronType = PatronType.Research;

        //Act
        Patron regularPatron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(name, regularPatron.Name);
        Assert.Equal(dateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(email, regularPatron.Email);
        Assert.Equal(address, regularPatron.Address);
        Assert.Equal(patronType, regularPatron.PatronType);
        Assert.IsType<EntityId>(regularPatron.Id);
    }

    [Fact]
    public void ForNonAllowedZipCodes_Create_Should_Throw_NotAllowedAddressException()
    {
        //Arrange
        string expectedExceptionMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode());
        PatronType patronType = PatronType.Regular;

        Patron regularPatron;
        List<Document> onboardingDocuments = [PersonalIdentification];

        //Act
        Action action = () =>
        {
            regularPatron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        NotAllowedAddressException exception = Assert.Throws<NotAllowedAddressException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForRegularPatron_Create_ThrowsMissingDocumentException_WhenPersonalIdentificationIsNotProvided()
    {

        //Arrange
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingPersonalIdentificationException exception = Assert.Throws<MissingPersonalIdentificationException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForResearchPatron_Create_Throws_MissingPersonalIdentificationException_WhenPersonalIdentificationIsNotProvided()
    {

        //Arrange
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Research;
        List<Document> onboardingDocuments = [];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingPersonalIdentificationException exception = Assert.Throws<MissingPersonalIdentificationException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForResearchPatron_Create_Throws_MissingAcademicIdentificationException_WhenAcademicIdentificationIsNotProvided()
    {

        //Arrange
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Research;
        List<Document> onboardingDocuments = [PersonalIdentification, AddressProof];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingAcademicsIdentificationException exception = Assert.Throws<MissingAcademicsIdentificationException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForRegularPatron_Create_ThrowsMissingAddressProofException_WhenAddressProofIsNotProvided()
    {

        //Arrange
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [PersonalIdentification];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingAddressProofException exception = Assert.Throws<MissingAddressProofException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForResearchPatron_Create_ThrowsMissingAddressProofException_WhenAddressProofIsNotProvided()
    {

        //Arrange
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Email email = new(Faker.Person.Email);
        Address address = Address.Create(Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [PersonalIdentification, AcademicsIdentification];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingAddressProofException exception = Assert.Throws<MissingAddressProofException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}
