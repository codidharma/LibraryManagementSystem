using LMS.Modules.Membership.API.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;
using LMS.Modules.Membership.UnitTests.Base;

namespace LMS.Modules.Membership.UnitTests.DomainTests;

public class PatronTests : TestBase
{

    [Fact]
    public void Create_Should_ReturnUser()
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

        //Act
        Patron regularPatron = Patron.Create(name, gender, dateOfBirth, address);

        //Assert
        Assert.NotNull(regularPatron);
        Assert.Equal(name, regularPatron.Name);
        Assert.Equal(dateOfBirth, regularPatron.DateOfBirth);
        Assert.Equal(address, regularPatron.Address);
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

        Patron regularPatron;

        //Act
        Action action = () => { regularPatron = Patron.Create(name, gender, dateOfBirth, address); };

        Assert.Throws<NotAllowedAddressException>(action);
    }
}
