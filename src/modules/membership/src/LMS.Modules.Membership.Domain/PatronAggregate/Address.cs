using LMS.Common.Domain;

namespace LMS.Modules.Membership.Domain.PatronAggregate;

public sealed record Address : ValueObject
{
    private const string ExceptionMessage = @$"Address should be composed of non null, empty of whitespace values for {nameof(BuildingNumber)}, {nameof(Street)}, {nameof(City)}, {nameof(State)} {nameof(Country)} and {nameof(ZipCode)}";

    private static readonly Error InvalidValuesError =
        new("Membership.InvalidValueError", ExceptionMessage, ErrorType.InvalidDomain);

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
        BuildingNumber = buildingNumber;
        Street = street;
        City = city;
        State = state;
        Country = country;
        ZipCode = zipCode;
    }

    public static Result<Address> Create(string buildingNumber,
                   string street,
                   string city,
                   string state,
                   string country,
                   string zipCode)
    {
        if (string.IsNullOrWhiteSpace(buildingNumber)
            || string.IsNullOrWhiteSpace(street)
            || string.IsNullOrWhiteSpace(city)
            || string.IsNullOrWhiteSpace(state)
            || string.IsNullOrWhiteSpace(country)
            || string.IsNullOrWhiteSpace(zipCode))
        {
            return Result.Failure<Address>(InvalidValuesError);
        }

        var address = new Address(buildingNumber, street, city, state, country, zipCode);
        return Result.Success<Address>(address);
    }
}
