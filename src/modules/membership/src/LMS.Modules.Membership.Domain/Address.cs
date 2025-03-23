using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.Exceptions;

namespace LMS.Modules.Membership.Domain;

public sealed class Address : Entity
{
    private const string ExceptionMessage = @$"Address should be composed of non null, empty of whitespace values for {nameof(Street)}, {nameof(City)}, {nameof(State)} {nameof(Country)} and {nameof(ZipCode)}";

    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }

    public Guid PatronId { get; }
    public string ZipCode { get; }

    private Address() { }
    private Address(Guid patronId,
                   string street,
                   string city,
                   string state,
                   string country,
                   string zipCode)
    {
        if (string.IsNullOrWhiteSpace(street)
            || string.IsNullOrWhiteSpace(city)
            || string.IsNullOrWhiteSpace(state)
            || string.IsNullOrWhiteSpace(country)
            || string.IsNullOrWhiteSpace(zipCode))
        {
            throw new InvalidValueException(ExceptionMessage);
        }
        PatronId = patronId;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Create(Guid patronId,
                   string street,
                   string city,
                   string state,
                   string country,
                   string zipCode)
    {
        Address address = new Address(patronId, street, city, state, country, zipCode);
        return address;
    }
}
