﻿using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.DomainEvents;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;



namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Patron : Entity
{
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }

    public Email Email { get; }
    public Address Address { get; private set; }

    public PatronType PatronType { get; }

    public List<Document> Documents { get; private set; }
    public AccessId AccessId { get; }

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

        return Result.Success();
    }

    public Result AddDocuments(List<Document> documents)
    {

        if (!IsPersonalIdentificationDocumentAvailable(documents))
        {
            Error error = Error.InvalidDomain(
                code: "Membership.InvalidDomainValue",
                description: $"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.");

            return Result.Failure(error);
        }
        if (!IsAddressProofDocumentAvailable(documents))
        {
            Error error = Error.InvalidDomain(
                 code: "Membership.InvalidDomainValue",
                 description: $"Document of type {DocumentType.AddressProof.Name} is mandatory.");

            return Result.Failure(error);
        }
        if (PatronType.Equals(PatronType.Research) && !IsAcademicsIdentificationDocumentAvailable(documents))
        {
            Error error = Error.InvalidDomain(
                code: "Membership.InvalidDomainValue",
                description: $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.");

            return Result.Failure(error);
        }

        Documents = documents;

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
