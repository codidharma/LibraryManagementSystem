using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GenerateCredentials;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GenerateCredentials;

internal sealed class GenerateCredentialsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/memberships/onboarding/patrons/{id}/credentials", async (
            Guid id,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator) =>
        {
            GenerateCredentialsCommand command = new(id);
            Result<CommandResponse> generateCredentialsResult = await dispatcher
            .DispatchAsync<GenerateCredentialsCommand, Result<CommandResponse>>(command, default);

            if (generateCredentialsResult.IsSuccess)
            {
                Response response = generateCredentialsResult.Value.ToDto();
                List<HypermediaLink> links = [

                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNames.GetPatronById,
                        values: new{id = id.ToString() })!, EndpointNames.GetPatronById, HttpMethods.Get),
                    ];

                response.Links = links;

                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(generateCredentialsResult);
        })
            .WithTags(Tags.Membership)
            .WithName(EndpointNames.GenerateCredentials);
    }
}
