namespace LMS.Modules.Membership.API.Common.Domain;

internal sealed class AddressValidationService
{
    private readonly List<string> _allowedZipCodes = ["412105", "411027"];
    public bool Validate(Address address)
    {
        return _allowedZipCodes.Contains(address.ZipCode);

    }
}
