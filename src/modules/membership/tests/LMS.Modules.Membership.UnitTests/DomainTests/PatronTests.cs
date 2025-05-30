using LMS.Modules.Membership.Domain.PatronAggregate.Constants;

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
            NationalId,
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
        Assert.Equal(PatronAdded, regularPatron.OnboardingStage);
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
            NationalId,
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
        Assert.Equal(KycPending, researchPatron.KycStatus);
        Assert.Equal(PatronInActive, researchPatron.Status);
        Assert.Equal(PatronAdded, researchPatron.OnboardingStage);
        Assert.IsType<EntityId>(researchPatron.Id);
    }

    [Fact]
    public void AddAddress_ShouldAdd_AddressToThePatronInstance()
    {
        //Arrange
        Domain.PatronAggregate.Address address = Domain.PatronAggregate.Address.Create(Faker.Address.BuildingNumber(),
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

        Domain.PatronAggregate.Address patronAddress = patron.Address;
        Assert.Equal(address, patronAddress);
        Assert.Equal(KycInProgress, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);
        Assert.Equal(AddressAdded, patron.OnboardingStage);

    }

    [Fact]
    public void ForNonAllowedZipCodes_AddAddress_ShouldReturnFailureResult()
    {
        //Arrange
        string expectedErrorMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        string expectedErrorCode = "Membership.InvalidDomainValue";

        Domain.PatronAggregate.Address address = Domain.PatronAggregate.Address.Create(Faker.Address.BuildingNumber(),
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
        Assert.Contains(PersonalIdentification, patron.Documents);
        Assert.Equal(DocumentAdded, patron.OnboardingStage);
    }

    [Fact]
    public void ForRegularPatron_VerifyDocuments_ShouldReturn_FailureResult_WhenPersonalIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = RegularPatron;


        //Act
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsFailure);
        Assert.False(verifyDocumentsResult.IsSuccess);

        Error error = verifyDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
        Assert.Equal(KycFailed, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);

    }

    [Fact]
    public void ForRegularPatron_VerifyDocuments_ShouldReturn_FailureResult_WhenAddressProofIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = RegularPatron;


        //Act
        foreach (Domain.PatronAggregate.Document document in PersonalIdentificationDocuments)
        {
            patron.AddDocument(document);
        }
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsFailure);
        Assert.False(verifyDocumentsResult.IsSuccess);

        Error error = verifyDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
        Assert.Equal(KycFailed, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);

    }

    [Fact]
    public void ForResearchPatron_VerifyDocuments_ShouldReturn_FailureResult_WhenPersonalIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.PersonalIdentification.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsFailure);
        Assert.False(verifyDocumentsResult.IsSuccess);

        Error error = verifyDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
        Assert.Equal(KycFailed, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);
    }

    [Fact]
    public void ForResearchPatron_VerifyDocuments_ShouldReturn_FailureResult_WhenAddressProofIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {Domain.PatronAggregate.DocumentType.AddressProof.Name} is mandatory.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        foreach (Domain.PatronAggregate.Document document in ResearchPatronMissingAddressProofDocuments)
        {
            patron.AddDocument(document);
        }
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsFailure);
        Assert.False(verifyDocumentsResult.IsSuccess);

        Error error = verifyDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
        Assert.Equal(KycFailed, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);
    }

    [Fact]
    public void ForResearchPatron_VerifyDocuments_ShouldReturn_FailureResult_WhenAcademicIdentificationIsNotProvided()
    {
        //Arrange
        string expectedErrorMessage = $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.";
        string expectedErrorCode = "Membership.InvalidDomainValue";
        Patron patron = ResearchPatron;


        //Act
        foreach (Domain.PatronAggregate.Document document in ResearchPatronMissingAcademicsDocuments)
        {
            patron.AddDocument(document);

        }
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsFailure);
        Assert.False(verifyDocumentsResult.IsSuccess);

        Error error = verifyDocumentsResult.Error;

        Assert.Equal(expectedErrorCode, error.Code);
        Assert.Equal(expectedErrorMessage, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
        Assert.Equal(KycFailed, patron.KycStatus);
        Assert.Equal(PatronInActive, patron.Status);
    }

    [Fact]
    public void ForRegularPatron_VerifyDocuments_ShouldReturn_SuccessResult_WhenRelevantDocumnentsAreProvided()
    {
        //Arrange
        Patron patron = RegularPatron;

        foreach (Domain.PatronAggregate.Document document in RegularPatronOnboardingDocuments)
        {
            patron.AddDocument(document);
        }

        //Act
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsSuccess);
        Assert.False(verifyDocumentsResult.IsFailure);
        Assert.Equal(KycCompleted, patron.KycStatus);
        Assert.Equal(PatronActive, patron.Status);
        Assert.Equal(DocumentsVerified, patron.OnboardingStage);
    }
    [Fact]
    public void ForResearchPatron_VerifyDocuments_ShouldReturn_SuccessResult_WhenRelevantDocumnentsAreProvided()
    {
        //Arrange
        Patron patron = ResearchPatron;

        foreach (Domain.PatronAggregate.Document document in ResearchPatronOnboardingDocuments)
        {
            patron.AddDocument(document);
        }

        //Act
        Result verifyDocumentsResult = patron.VerifyDocuments();

        //Assert
        Assert.True(verifyDocumentsResult.IsSuccess);
        Assert.False(verifyDocumentsResult.IsFailure);
        Assert.Equal(KycCompleted, patron.KycStatus);
        Assert.Equal(PatronActive, patron.Status);
        Assert.Equal(DocumentsVerified, patron.OnboardingStage);
    }

    [Fact]
    public void SetAccessId_Should_SetTheAccessIdAndCompleteOnboarding()
    {
        //Arrange
        Guid accessId = Guid.NewGuid();
        Patron patron = RegularPatron;

        //Act
        Result setAccessIdResult = patron.SetAccessId(accessId);

        //Assert
        Assert.True(setAccessIdResult.IsSuccess);
        Assert.False(setAccessIdResult.IsFailure);
        Assert.Equal(accessId, patron.AccessId.Value);
        Assert.Equal(Completed, patron.OnboardingStage);
    }

    [Fact]
    public void ForEmptyAccessId_SetAccessId_Should_ReturnFailureResult()
    {
        //Arrange
        string errorCode = ErrorCodes.InvalidDomainValue;
        string errorDescription = "Invalid AccessId. Guid can not comprise of zeros only.";
        Guid accessId = Guid.Empty;
        Patron patron = RegularPatron;

        //Act
        Result setAccessIdResult = patron.SetAccessId(accessId);

        //Asser
        Assert.True(setAccessIdResult.IsFailure);
        Assert.False(setAccessIdResult.IsSuccess);
        Error error = setAccessIdResult.Error;
        Assert.Equal(errorCode, error.Code);
        Assert.Equal(errorDescription, error.Description);
        Assert.Equal(ErrorType.InvalidDomain, error.ErrorType);
    }
}
