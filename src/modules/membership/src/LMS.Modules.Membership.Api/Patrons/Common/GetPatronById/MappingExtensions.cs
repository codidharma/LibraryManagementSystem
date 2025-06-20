﻿using LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;

namespace LMS.Modules.Membership.Api.Patrons.Common.GetPatronById;

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
            NationalId: getPatronByIdQueryResponse.NationalId,
            PatronType: getPatronByIdQueryResponse.PatronType
            );
        return response;
    }
}
