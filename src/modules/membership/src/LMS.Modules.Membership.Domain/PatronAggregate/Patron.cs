using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Common;
using LMS.Modules.Membership.Domain.PatronAggregate.Constants;



namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Patron : Entity
{
#pragma warning disable IDE0032 // Use auto property
    private readonly List<Document> _documents = [];
#pragma warning restore IDE0032 // Use auto property
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }

    public Email Email { get; }

    public NationalId NationalId { get; }
    public Address Address { get; private set; }

    public PatronType PatronType { get; }

    public List<Document> Documents => _documents;
    public AccessId AccessId { get; private set; }

    public KycStatus KycStatus { get; private set; }

    public Status Status { get; private set; }

    public OnboardingStage OnboardingStage { get; private set; }
    private Patron() { }

    private Patron(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        NationalId nationalId,
        PatronType patronType)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Email = email;
        NationalId = nationalId;
        PatronType = patronType;
        KycStatus = KycStatus.Pending;
        Status = Status.InActive;
        OnboardingStage = OnboardingStage.PatronAdded;
    }

    public static Result<Patron> Create(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        NationalId nationalId,
        PatronType patronType)
    {
        Patron patron = new(
            name,
            gender,
            dateOfBirth,
            email,
            nationalId,
            patronType);

        return Result.Success(patron);
    }

    public Result AddAddress(Address address)
    {
        Address = address;

        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        if (!isAddressAllowed)
        {
            Error error = Error.InvalidDomain(
                code: ErrorCodes.InvalidDomainValue,
                description: $"The value for property {nameof(address.ZipCode)} is not allowed.");

            return Result.Failure(error);
        }
        KycStatus = KycStatus.InProgress;
        OnboardingStage = OnboardingStage.AddressAdded;
        return Result.Success();
    }

    public Result VerifyDocuments()
    {
        if (!IsPersonalIdentificationDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                code: ErrorCodes.InvalidDomainValue,
                description: $"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        if (!IsAddressProofDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                 code: ErrorCodes.InvalidDomainValue,
                 description: $"Document of type {DocumentType.AddressProof.Name} is mandatory.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        if (PatronType.Equals(PatronType.Research) && !IsAcademicsIdentificationDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                code: ErrorCodes.InvalidDomainValue,
                description: $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        KycStatus = KycStatus.Completed;
        Status = Status.Active;
        OnboardingStage = OnboardingStage.DocumentsVerified;
        return Result.Success();
    }

    public Result AddDocument(Document document)
    {
        _documents.Add(document);
        OnboardingStage = OnboardingStage.DocumentAdded;

        return Result.Success();
    }

    public Result SetAccessId(Guid accessId)
    {

        Result<AccessId> accessIdCreateResult = AccessId.Create(accessId);
        if (accessIdCreateResult.IsFailure)
        {
            return Result.Failure(accessIdCreateResult.Error);
        }
        AccessId = accessIdCreateResult.Value;
        OnboardingStage = OnboardingStage.Completed;
        return Result.Success();
    }


    private static bool IsPersonalIdentificationDocumentAvailable(List<Document> identityDocuments)
    {
        DocumentType personalIdentifcation = DocumentType.PersonalIdentification;

        return identityDocuments.Any(x => x.DocumentType == personalIdentifcation);
    }

    private static bool IsAcademicsIdentificationDocumentAvailable(List<Document> identityDocuments)
    {
        DocumentType academicsIdentification = DocumentType.AcademicsIdentification;

        return identityDocuments.Any(x => x.DocumentType == academicsIdentification);
    }

    private static bool IsAddressProofDocumentAvailable(List<Document> documents)
    {
        DocumentType addressProof = DocumentType.AddressProof;
        return documents.Any(doc => doc.DocumentType == addressProof);
    }
}
