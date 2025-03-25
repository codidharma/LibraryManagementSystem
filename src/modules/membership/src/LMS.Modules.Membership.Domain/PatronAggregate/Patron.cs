using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;



namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Patron : Entity
{
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }

    public Email Email { get; }
    public Address Address { get; }

    public PatronType PatronType { get; }

    public List<Document> IdentityDocuments { get; }
    public AccessId AccessId { get; }

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
        IdentityDocuments = identityDocuments;
        AccessId = accessId;
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

        Patron regularPatron = new(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            identityDocuments,
            accessId);

        return regularPatron;
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
