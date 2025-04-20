using LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : PatronTestBase
{
    [Fact]
    public void Create_Should_ReturnRegularPatron()
    {
        //Act
        Result<Patron> regularPatronResult = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            RegularPatronType);

        //Assert
        Assert.True(regularPatronResult.IsSuccess);
        Assert.False(regularPatronResult.IsFailure);

        Patron regularPatron = regularPatronResult.Value;
        Assert.NotNull(regularPatron);
        Assert.Equal(Name, regularPatron.Name);
        Assert.Equal(DateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(RegularPatronType, regularPatron.PatronType);
        Assert.Equal(Email, regularPatron.Email);
        Assert.Equal(KycPending, regularPatron.KycStatus);
        Assert.Equal(PatronInActive, regularPatron.Status);
        Assert.IsType<EntityId>(regularPatron.Id);
    }

    [Fact]
    public void Create_Should_ReturnReSearchPatron()
    {
        //Act
        Result<Patron> researchPatronResult = Patron.Create(
            Name,
            Gender,
            DateOfBirth,
            Email,
            ResearchPatronType);

        //Assert
        Assert.True(researchPatronResult.IsSuccess);
        Assert.False(researchPatronResult.IsFailure);

        Patron researchPatron = researchPatronResult.Value;
        Assert.NotNull(researchPatron);
        Assert.Equal(Name, researchPatron.Name);
        Assert.Equal(DateOfBirth, researchPatron.DateOfBirth);
        Assert.Equal(Email, researchPatron.Email);
        Assert.Equal(ResearchPatronType, researchPatron.PatronType);
        Assert.IsType<EntityId>(researchPatron.Id);
    }

    [Fact]
    public void AddAddress_ShouldAdd_AddressToThePatronInstance()
    {
        //Arrange
        Address address = Address.Create(Faker.Address.BuildingNumber(),
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105").Value;

        Patron patron = RegularPatron;

        //Assert
        Result addAddressResult = patron.AddAddress(address);

        //Assert
        Assert.True(addAddressResult.IsSuccess);
        Assert.False(addAddressResult.IsFailure);
    }

    [Fact]
    public void ForNonAllowedZipCodes_AddAddress_ShouldReturnFailureResult()
    {
        //Arrange
        string expectedErrorMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        string expectedErrorCode = "Membership.InvalidDomainValue";

        Address address = Address.Create(Faker.Address.BuildingNumber(),
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode()).Value;

        Patron patron = RegularPatron;

        //Act
        Result addAddressResult = patron.AddAddress(address);

        //Assert
        Assert.True(addAddressResult.IsFailure);
        Assert.False(addAddressResult.IsSuccess);

        Error error = addAddressResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void AddDocument_ShouldAdd_DocumentToListOfDocumentsOfPatron()
    {
        //Arrange
        Patron patron = RegularPatron;

        //Act
        Result addDocumentResult = patron.AddDocument(PersonalIdentification);

        //Assert
        Assert.True(addDocumentResult.IsSuccess);
        Assert.False(addDocumentResult.IsFailure);
    }

    [Fact]
    public void ForRegularPatron_AddDocuments_ShouldReturn_FailureResult_WhenPersonalIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = RegularPatron;


        //Act
        Result addDocumentsResult = patron.AddDocuments([]);

        //Assert
        Assert.True(addDocumentsResult.IsFailure);
        Assert.False(addDocumentsResult.IsSuccess);

        Error error = addDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void ForRegularPatron_AddDocuments_ShouldReturn_FailureResult_WhenAddressProofIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = RegularPatron;


        //Act
        Result addDocumentsResult = patron.AddDocuments(PersonalIdentificationDocuments);

        //Assert
        Assert.True(addDocumentsResult.IsFailure);
        Assert.False(addDocumentsResult.IsSuccess);

        Error error = addDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void ForResearchPatron_AddDocuments_ShouldReturn_FailureResult_WhenPersonalIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        Result addDocumentsResult = patron.AddDocuments([]);

        //Assert
        Assert.True(addDocumentsResult.IsFailure);
        Assert.False(addDocumentsResult.IsSuccess);

        Error error = addDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void ForResearchPatron_AddDocuments_ShouldReturn_FailureResult_WhenAddressProofIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        Result addDocumentsResult = patron.AddDocuments(ResearchPatronMissingAddressProofDocuments);

        //Assert
        Assert.True(addDocumentsResult.IsFailure);
        Assert.False(addDocumentsResult.IsSuccess);

        Error error = addDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    [Fact]
    public void ForResearchPatron_AddDocuments_ShouldReturn_FailureResult_WhenAcademicIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        Result addDocumentsResult = patron.AddDocuments(ResearchPatronMissingAcademicsDocuments);

        //Assert
        Assert.True(addDocumentsResult.IsFailure);
        Assert.False(addDocumentsResult.IsSuccess);

        Error error = addDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }

    #region toberemoved
    [Fact]
    public void ForNonAllowedZipCodes_Create_Should_Throw_NotAllowedAddressException()
    {
        //Arrange
        Patron regularPatron;
        string expectedExceptionMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        Address address = Address.Create(Faker.Address.BuildingNumber(),
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            Faker.Address.ZipCode()).Value;

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
    #endregion
}
