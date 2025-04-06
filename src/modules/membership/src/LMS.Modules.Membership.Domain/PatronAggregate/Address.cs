using LMS.Common.Domain;
using LMS.Modules.Membership.Domain.PatronAggregate.Exceptions;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed class Address : Entity
{
    private const string ExceptionMessage = @$"Address should be composed of non null, empty of whitespace values for {nameof(BuildingNumber)}, {nameof(Street)}, {nameof(City)}, {nameof(State)} {nameof(Country)} and {nameof(ZipCode)}";

    public string BuildingNumber { get; }
    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string Country { get; }
    public string ZipCode { get; }

    private Address() { }
    private Address(string buildingNumber,
                   string street,
                   string city,
                   string state,
                   string country,
                   string zipCode)
    {
        if (string.IsNullOrEmpty(buildingNumber)
            || string.IsNullOrWhiteSpace(street)
            || string.IsNullOrWhiteSpace(city)
            || string.IsNullOrWhiteSpace(state)
            || string.IsNullOrWhiteSpace(country)
            || string.IsNullOrWhiteSpace(zipCode))
        {
            throw new InvalidValueException(ExceptionMessage);
        }

        BuildingNumber = buildingNumber;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Address Create(string buildingNumber,
                   string street,
                   string city,
                   string state,
                   string country,
                   string zipCode)
    {
        var address = new Address(buildingNumber, street, city, state, country, zipCode);
        return address;
    }
}
