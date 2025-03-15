using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : TestBase
{

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

        //Act
        Patron regularPatron = Patron.Create(name, gender, dateOfBirth, address, patronType);

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

        PatronType patronType = PatronType.Research;

        //Act
        Patron regularPatron = Patron.Create(name, gender, dateOfBirth, address, patronType);

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

        //Act
        Action action = () => { regularPatron = Patron.Create(name, gender, dateOfBirth, address, patronType); };

        Assert.Throws<NotAllowedAddressException>(action);
    }
}
