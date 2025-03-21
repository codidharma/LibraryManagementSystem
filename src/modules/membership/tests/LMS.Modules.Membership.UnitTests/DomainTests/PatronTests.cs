﻿using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : TestBase
{
    private readonly Document PersonalIdentification = Document.Create(
        DocumentType.PersonalIdentification,
        new("somedata", DocumentContentType.Pdf));
    private readonly Document AcademicsIdentification = Document.Create(
        DocumentType.AcademicsIdentification,
        new("somedata", DocumentContentType.Pdf));

    [Fact]
    public void Create_Should_ReturnRegularPatron()
    {
        //Arrange
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [PersonalIdentification];


        //Act
        Patron regularPatron = Patron.Create(name, gender, dateOfBirth, address, patronType, onboardingDocuments);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(name, regularPatron.Name);
        Assert.Equal(dateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(address, regularPatron.Address);
        Assert.Equal(patronType, regularPatron.PatronType);
    }

    [Fact]
    public void Create_Should_ReturnReSearchPatron()
    {
        //Arrange
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");
        List<Document> onboardingDocuments = [PersonalIdentification, AcademicsIdentification];

        PatronType patronType = PatronType.Research;

        //Act
        Patron regularPatron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            address,
            patronType,
            onboardingDocuments);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(name, regularPatron.Name);
        Assert.Equal(dateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(address, regularPatron.Address);
        Assert.Equal(patronType, regularPatron.PatronType);
    }

    [Fact]
    public void ForNonAllowedZipCodes_Create_Should_Throw_NotAllowedAddressException()
    {
        //Arrange
        string expectedExceptionMessage = $"The value for property {nameof(Address.ZipCode)} is not allowed.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
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
        string expectedExceptionMessage = $"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
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
        string expectedExceptionMessage = $"Document of type {DocumentType.PersonalIdentification.Name} is mandatory.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
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
        string expectedExceptionMessage = $"Document of type {DocumentType.AcademicsIdentification.Name} is mandatory for a research patron.";
        Name name = new(Faker.Person.FullName);
        Gender gender = new(Faker.Person.Gender.ToString());
        DateOfBirth dateOfBirth = new(Faker.Person.DateOfBirth);
        Address address = new(
            Faker.Address.StreetName(),
            Faker.Address.City(),
            Faker.Address.State(),
            Faker.Address.Country(),
            "412105");

        PatronType patronType = PatronType.Research;
        List<Document> onboardingDocuments = [PersonalIdentification];

        Patron patron;

        //Assert
        Action action = () =>
        {
            patron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            address,
            patronType,
            onboardingDocuments);
        };

        //Assert
        MissingAcademicsIdentificationException exception = Assert.Throws<MissingAcademicsIdentificationException>(action);
        Assert.Equal(expectedExceptionMessage, exception.Message);
    }
}
