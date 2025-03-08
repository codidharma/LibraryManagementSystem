using LMS.Common.Domain;
using LMS.Modules.Membership.API.Common.Domain.Exceptions;

namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class RegularPatron : Entity
{
    public Name Name { get; }
    public Gender Gender { get; }
    public DateOfBirth DateOfBirth { get; }
    public Address Address { get; }

    private RegularPatron() { }
    private RegularPatron(
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

    public static RegularPatron Create(
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

        RegularPatron regularPatron = new(name, gender, dateOfBirth, address);

        return regularPatron;
    }


}
