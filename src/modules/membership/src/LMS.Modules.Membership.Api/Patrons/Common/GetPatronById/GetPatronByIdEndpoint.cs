using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Query;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Common.GetPatronById;

internal sealed class GetPatronByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/memberships/patrons/{id}", async (Guid id, IQueryDispatcher dispatcher) =>
        {

            Result<GetPatronByIdQueryResponse> queryResponseResult = await dispatcher
            .DispatchAsync<Guid, Result<GetPatronByIdQueryResponse>>(id, default);

            if (queryResponseResult.IsSuccess)
            {
                GetPatronByIdQueryResponse queryResponse = queryResponseResult.Value;
                Response response = new(
                    Id: queryResponse.Id,
                    Name: queryResponse.Name,
                    Gender: queryResponse.Gender,
                    DateOfBirth: queryResponse.DateOfBirth,
                    Email: queryResponse.Email,
                    NationalId: queryResponse.NationalId,
                    PatronType: queryResponse.PatronType);

                return TypedResults.Ok(response);

            }
            return ProblemFactory.Create(queryResponseResult);
        })
        .AllowAnonymous()
        .WithName(EndpointNames.GetPatronById)
        .WithTags(Tags.Membership);
    }
}
