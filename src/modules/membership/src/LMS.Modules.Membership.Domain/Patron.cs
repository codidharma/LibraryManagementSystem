﻿using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Exceptions;



namespace LMS.Modules.Membership.Domain;

public sealed class Patron : Entity
{
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }
    public Address Address { get; }

    public PatronType PatronType { get; }

    public List<Document> IdentityDocuments { get; }

    private Patron() { }
    private Patron(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Address address,
        PatronType patronType,
        List<Document> identityDocuments)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
        PatronType = patronType;
        IdentityDocuments = identityDocuments;
    }

    public static Patron Create(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Address address,
        PatronType patronType,
        List<Document> identityDocuments)
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

        if (patronType.Equals(PatronType.Research) && !IsAcademicsIdentificationDocumentAvailable(identityDocuments))
        {
            throw new MissingAcademicsIdentificationException($"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.");
        }

        Patron regularPatron = new(name, gender, dateOfBirth, address, patronType, identityDocuments);

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
}
