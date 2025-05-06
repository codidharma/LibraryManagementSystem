using LMS.Modules.Membership.Application.Patrons.Onboarding.GetAddressByPatronId;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetAddressByPatronId;

internal static class MappingExtensions
{
    public static Response ToDto(this GetAddressByPatronIdQueryResponse queryResponse)
    {
        Response response = new(
            queryResponse.BuildingNumber,
            queryResponse.StreetName,
            queryResponse.City,
            queryResponse.State,
            queryResponse.Country,
            queryResponse.ZipCode
            );
        return response;
    }
}
