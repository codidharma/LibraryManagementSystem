using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GenerateCredentials;

internal sealed class GenerateCredentialsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/membership/onboarding/patron/{id}/credentials", async (
            [FromRoute] Guid id,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGerneator) =>
        {
            GenerateCredentialsCommand command = new(id);
            Result<CommandResponse> generateCredentialsResult = await dispatcher
            .DispatchAsync<GenerateCredentialsCommand, Result<CommandResponse>>(command, default);

            if (generateCredentialsResult.IsSuccess)
            {
                Response response = generateCredentialsResult.Value.ToDto();
                List<HypermediaLink> links = [

                    new(linkGerneator.GetUriByName(
                        httpContext,
                        EndpointNamesConstants.GetPatronById,
                        values: new{id = id.ToString() })!, EndpointNamesConstants.GetPatronById, HttpMethodConstants.Get),
                    ];

                response.Links = links;

                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(generateCredentialsResult);
        })
            .WithTags(Tags.Membership)
            .WithName(EndpointNamesConstants.GenerateCredentials);
    }
}
