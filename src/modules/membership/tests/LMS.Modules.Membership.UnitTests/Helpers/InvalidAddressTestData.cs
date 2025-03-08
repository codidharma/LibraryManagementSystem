using System.Collections;
using Bogus;

namespace LMS.Modules.Membership.UnitTests.Helpers;

public class InvalidAddressTestData : IEnumerable<object[]>
{
    private readonly Faker _faker = new();
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            _faker.Address.StreetName(),
            _faker.Address.City(),
            _faker.Address.State(),
            string.Empty,
            string.Empty
        };

        yield return new object[]
        {
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
