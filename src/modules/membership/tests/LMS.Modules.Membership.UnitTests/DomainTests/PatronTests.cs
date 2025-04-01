using LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : PatronTestBase
{
    [Fact]
    public void Create_Should_ReturnRegularPatron()
    {
        //Act
        Patron regularPatron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            RegularPatronType,
            RegularPatronOnboardingDocuments,
            AccessId);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(Name, regularPatron.Name);
        Assert.Equal(DateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(Address, regularPatron.Address);
        Assert.Equal(RegularPatronType, regularPatron.PatronType);
        Assert.Equal(Email, regularPatron.Email);
        Assert.Equal(AccessId, regularPatron.AccessId);
        Assert.IsType<EntityId>(regularPatron.Id);
    }

    [Fact]
    public void Create_Should_ReturnReSearchPatron()
    {
        //Act
        Patron researchPatron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            ResearchPatronType,
            ResearchPatronOnboardingDocuments,
            AccessId);

        //Assert
        Assert.NotNull(researchPatron);
        Assert.Equal(Name, researchPatron.Name);
        Assert.Equal(DateOfBirth, researchPatron.DateOfBirth);
        Assert.Equal(Email, researchPatron.Email);
        Assert.Equal(Address, researchPatron.Address);
        Assert.Equal(ResearchPatronType, researchPatron.PatronType);
        Assert.Equal(AccessId, researchPatron.AccessId);
        Assert.IsType<EntityId>(researchPatron.Id);
    }

    [Fact]
    public void ForNonAllowedZipCodes_Create_Should_Throw_NotAllowedAddressException()
    {
        //Arrange
        Patron regularPatron;
        string expectedExceptionMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        Address address = Address.Create(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode());

        //Act
        Action action = () =>
            {
                regularPatron = Patron.Create(
                Name,
                Gender,
                DateOfBirth,
                Email,
                address,
                RegularPatronType,
                RegularPatronOnboardingDocuments,
                AccessId);
            };

        NotAllowedAddressException exception = Assert.Throws<NotAllowedAddressException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForRegularPatron_Create_ThrowsMissingDocumentException_WhenPersonalIdentificationIsNotProvided()
    {

        //Arrange
        Patron patron;
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            RegularPatronType,
            [],
            AccessId);
        };

        //Assert
        MissingPersonalIdentificationException exception = Assert.Throws<MissingPersonalIdentificationException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void ForResearchPatron_Create_Throws_MissingPersonalIdentificationException_WhenPersonalIdentificationIsNotProvided()
    {

        //Arrange
        Patron patron;
        string expectedExceptionMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            ResearchPatronType,
            [],
            AccessId);
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
        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            ResearchPatronType,
            ResearchPatronMissingAcademicsDocuments,
            AccessId);
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
        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            RegularPatronType,
            PersonalIdentificationDocuments,
            AccessId);
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
        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            ResearchPatronType,
            ResearchPatronMissingAddressProofDocuments,
            AccessId);
        };

        //Assert
        MissingAddressProofException exception = Assert.Throws<MissingAddressProofException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }

    [Fact]
    public void CreatingRegularPatron_Raises_PatronCreatedDomainEvent()
    {
        //Arrange
        string expectedPatronType = PatronType.Regular.Name;
        //Act
        Patron regularPatron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            RegularPatronType,
            RegularPatronOnboardingDocuments,
            AccessId
            );

        Assert.NotEmpty(regularPatron.DomainEvents);
        PatronCreatedDomainEvent domainEvent = (PatronCreatedDomainEvent)regularPatron
            .DomainEvents.FirstOrDefault(e => e is PatronCreatedDomainEvent);
        Assert.NotNull(domainEvent);
        Assert.Equal(expectedPatronType, domainEvent.PatronType);

    }

    [Fact]
    public void CreatingResearchPatron_Raises_PatronCreatedDomainEvent()
    {
        //Arrange
        string expectedPatronType = PatronType.Research.Name;
        //Act
        Patron regularPatron = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            Address,
            ResearchPatronType,
            ResearchPatronOnboardingDocuments,
            AccessId
            );

        Assert.NotEmpty(regularPatron.DomainEvents);
        PatronCreatedDomainEvent domainEvent = (PatronCreatedDomainEvent)regularPatron
            .DomainEvents.FirstOrDefault(e => e is PatronCreatedDomainEvent);
        Assert.NotNull(domainEvent);
        Assert.Equal(expectedPatronType, domainEvent.PatronType);

    }
}
