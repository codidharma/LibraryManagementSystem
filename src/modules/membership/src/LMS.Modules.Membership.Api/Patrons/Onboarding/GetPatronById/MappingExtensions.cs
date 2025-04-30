using LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetPatronById;

internal static class MappingExtensions
{
    public static Response ToDto(this GetPatronByIdQueryResponse getPatronByIdQueryResponse)
    {
        Response response = new(
            Id: getPatronByIdQueryResponse.Id,
            Name: getPatronByIdQueryResponse.Name,
            Gender: getPatronByIdQueryResponse.Gender,
            DateOfBirth: getPatronByIdQueryResponse.DateOfBirth,
            Email: getPatronByIdQueryResponse.Email,
            PatronType: getPatronByIdQueryResponse.PatronType
            );
        return response;
    }
}
