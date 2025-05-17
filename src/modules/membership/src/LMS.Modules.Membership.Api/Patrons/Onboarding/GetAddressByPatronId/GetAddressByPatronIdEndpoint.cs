using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Query;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetAddressByPatronId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetPatronById;

internal sealed class GetAddressByPatronIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/membership/onboarding/patron/{id}/address", async ([FromRoute] Guid id, IQueryDispatcher dispatcher) =>
        {

            Result<GetAddressByPatronIdQueryResponse> queryResponseResult = await dispatcher
            .DispatchAsync<Guid, Result<GetAddressByPatronIdQueryResponse>>(id, default);


            return queryResponseResult.Match(Results.Ok, ProblemFactory.Create);
        })
        .AllowAnonymous()
        .WithName(nameof(GetAddressByPatronIdEndpoint))
        .WithTags(Tags.Membership);
    }
}
