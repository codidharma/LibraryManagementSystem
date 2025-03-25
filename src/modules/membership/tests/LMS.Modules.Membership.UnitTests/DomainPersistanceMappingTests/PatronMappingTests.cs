using Bogus;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainPersistanceMappingTests;

public class PatronMappingTests : TestBase
{
    private readonly Document PersonalIdentification = Document.Create(
        Domain.PatronAggregate.DocumentType.PersonalIdentification,
        new("somedata", DocumentContentType.Pdf));
    private readonly Document AddressProof = Document.Create(
        Domain.PatronAggregate.DocumentType.AddressProof,
        new("somedata", DocumentContentType.Pdf));

    [Fact]
    public void ToDao_ShouldReturn_DataAccessObject()
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

        PatronType patronType = PatronType.Regular;
        List<Document> onboardingDocuments = [PersonalIdentification, AddressProof];


        //Act
        Patron regularPatron = Patron.Create(
            name,
            gender,
            dateOfBirth,
            email,
            address,
            patronType,
            onboardingDocuments);
        //Act
        PatronDao dao = regularPatron.ToDao();

        //Assert
        Assert.Equal(regularPatron.Id.Value, dao.Id);
        Assert.Equal(name.Value, dao.Name);
        Assert.Equal(gender.Value, dao.Gender);
        Assert.Equal(dateOfBirth.Value, dao.DateOfBirth);
        Assert.Equal(email.Value, dao.Email);
        Assert.Equal(address.Street, dao.Address.Street);
        Assert.Equal(address.City, dao.Address.City);
        Assert.Equal(address.State, dao.Address.State);
        Assert.Equal(address.Country, dao.Address.Country);
        Assert.Equal(address.ZipCode, dao.Address.ZipCode);
        Assert.Equal(patronType.Name, dao.PatronType);
        Assert.Equal(onboardingDocuments.Count, dao.IdentityDocuments.Count);
    }

    [Fact]
    public void ToDomainModel_ShouldReturn_DomainModelObject()
    {
        //Arrange

        string name = Faker.Person.FullName;
        DateTime dateOfBirth = Faker.Person.DateOfBirth;
        string email = Faker.Person.Email;
        string gender = Faker.Person.Gender.ToString();
        string patronType = "Regular";
        AddressDao address = new()
        {
            Id = Guid.NewGuid(),
            Street = Faker.Address.StreetName(),
            City = Faker.Address.City(),
            State = Faker.Address.State(),
            Country = Faker.Address.Country(),
            ZipCode = "412105"
        };

        string content = "This is sample text";
        string contentType = "application/pdf";
        List<DocumentDao> documents = [
            new()
            {
                Id = Guid.NewGuid(),
                Content = content,
                ContentType = contentType,
                DocumentType = "PersonalId"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Content = content,
                ContentType = contentType,
                DocumentType = "AddressProof"
            }
            ];

        PatronDao dao = new()
        {
            Id = Guid.NewGuid(),
            Address = address,
            DateOfBirth = dateOfBirth,
            Email = email,
            Name = name,
            Gender = gender,
            PatronType = patronType,
            IdentityDocuments = documents
        };

        //Act
        Patron domainModel = dao.ToDomainModel();

        //Assert
        Assert.Equal(dao.Id, domainModel.Id.Value);
        Assert.Equal(dao.Name, domainModel.Name.Value);
        Assert.Equal(dao.Gender, domainModel.Gender.Value);
        Assert.Equal(dao.DateOfBirth, domainModel.DateOfBirth.Value);
        Assert.Equal(dao.Email, domainModel.Email.Value);
        Assert.Equal(dao.PatronType, domainModel.PatronType.Name);
        Assert.Equal(dao.Address.Street, domainModel.Address.Street);
        Assert.Equal(dao.Address.City, domainModel.Address.City);
        Assert.Equal(dao.Address.State, domainModel.Address.State);
        Assert.Equal(dao.Address.Country, domainModel.Address.Country);
        Assert.Equal(dao.Address.ZipCode, domainModel.Address.ZipCode);
        Assert.Equal(dao.IdentityDocuments.Count, domainModel.IdentityDocuments.Count);
    }
}
