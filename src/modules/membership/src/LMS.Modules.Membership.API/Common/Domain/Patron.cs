using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class Patron : Entity
{
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }
    public Address Address { get; }

    private Patron() { }
    private Patron(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Address address)
    {
        Name = name;
        Gender = gender;
        DateOfBirth = dateOfBirth;
        Address = address;
    }

    public static Patron Create(
        Name name,
        Gender gender,
        DateOfBirth dateOfBirth,
        Address address)
    {
        AddressValidationService addressValidationService = new();
        bool isAddressAllowed = addressValidationService.Validate(address);

        if (!isAddressAllowed)
        {
            throw new NotAllowedAddressException($"The value for property {nameof(address.ZipCode)} is not allowed.");
        }

        Patron regularPatron = new(name, gender, dateOfBirth, address);

        return regularPatron;
    }


}
