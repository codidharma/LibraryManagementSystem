using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;



namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Patron : Entity
{
    private readonly List<Document> _documents = [];
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }

    public Email Email { get; }
    public Address Address { get; private set; }

    public PatronType PatronType { get; }

    public List<Document> Documents { get; private set; }
    public AccessId AccessId { get; }

    public KycStatus KycStatus { get; private set; }

    public Status Status { get; private set; }
    private Patron() { }
    private Patron(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        Address address,
        PatronType patronType,
        List<Document> identityDocuments,
        AccessId accessId)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Email = email;
        Address = address;
        PatronType = patronType;
        Documents = identityDocuments;
        AccessId = accessId;
        KycStatus = KycStatus.Pending;
        Status = Status.InActive;
    }

    private Patron(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        PatronType patronType)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Email = email;
        PatronType = patronType;
        KycStatus = KycStatus.Pending;
        Status = Status.InActive;
    }

    public static Result<Patron> Create(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        PatronType patronType)
    {
        Patron patron = new(
            name,
            gender,
            dateOfBirth,
            email,
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
                code: "Membership.InvalidDomainValue",
                description: $"The value for property {nameof(address.ZipCode)} is not allowed.");

            return Result.Failure(error);
        }
        KycStatus = KycStatus.InProgress;
        return Result.Success();
    }

    public Result VerifyDocuments()
    {
        if (!IsPersonalIdentificationDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                code: "Membership.InvalidDomainValue",
                description: $"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        if (!IsAddressProofDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                 code: "Membership.InvalidDomainValue",
                 description: $"Document of type {DocumentType.AddressProof.Name} is mandatory.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        if (PatronType.Equals(PatronType.Research) && !IsAcademicsIdentificationDocumentAvailable(_documents))
        {
            Error error = Error.InvalidDomain(
                code: "Membership.InvalidDomainValue",
                description: $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.");
            KycStatus = KycStatus.Failed;
            return Result.Failure(error);
        }
        KycStatus = KycStatus.Completed;
        Status = Status.Active;
        return Result.Success();
    }

    public Result AddDocument(Document document)
    {
        _documents.Add(document);

        return Result.Success();
    }

    public static Patron Create(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Email email,
        Address address,
        PatronType patronType,
        List<Document> identityDocuments,
        AccessId accessId)
    {
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        if (!isAddressAllowed)
        {
            throw new NotAllowedAddressException($"The value for property {nameof(address.ZipCode)} is not allowed.");
        }

        if (!IsPersonalIdentificationDocumentAvailable(identityDocuments))
        {
            throw new MissingPersonalIdentificationException($"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.");
        }

        if (!IsAddressProofDocumentAvailable(identityDocuments))
        {
            throw new MissingAddressProofException($"Document of type {DocumentType.AddressProof.Name} is mandatory.");
        }

        if (patronType.Equals(PatronType.Research)
            && !IsAcademicsIdentificationDocumentAvailable(identityDocuments))
        {
            throw new MissingAcademicsIdentificationException($"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.");
        }

        Patron patron = new(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            identityDocuments,
            accessId);

        PatronCreatedDomainEvent patronCreatedDomainEvent = new(Guid.NewGuid(), DateTime.UtcNow, patronType.Name);
        patron.Raise(patronCreatedDomainEvent);

        return patron;
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
