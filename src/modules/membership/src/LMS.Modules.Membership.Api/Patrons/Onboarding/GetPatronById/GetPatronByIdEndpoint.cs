using LMS.Common.Application.Dispatchers;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetPatronById;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetPatronById;

internal sealed class GetPatronByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/membership/onboarding/patron/{id}", async ([FromRoute] Guid id, IQueryDispatcher dispatcher) =>
        {

            Result<GetPatronByIdQueryResponse> queryResponseResult = await dispatcher
            .DispatchAsync<Guid, Result<GetPatronByIdQueryResponse>>(id, default);


            return TypedResults.Ok(queryResponseResult.Value);
        })
        .AllowAnonymous()
        .WithName(nameof(GetPatronByIdEndpoint))
        .WithTags(Tags.Membership);
    }
}
