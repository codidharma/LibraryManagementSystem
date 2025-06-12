using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/memberships/onboarding/patrons", async (
            [FromBody] Request request,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator) =>
        {
            AddPatronCommand command = request.ToCommand();

            Result<CommandResult> addPatronResult = await dispatcher
            .DispatchAsync<AddPatronCommand, Result<CommandResult>>(command, default);

            if (addPatronResult.IsSuccess)
            {
                Response response = addPatronResult.Value.ToDto();

                List<HypermediaLink> links = [

                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNames.GetPatronById,
                        values: new{id = response.Id.ToString() })!, "self", HttpMethods.Get),
                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNames.AddAddress,
                        values: new{id = response.Id.ToString() })!, EndpointNames.AddAddress, HttpMethods.Put)

                    ];

                response.Links = links;

                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(addPatronResult);
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership)
        .WithName(EndpointNames.AddPatron)
        .WithMetadata(new EndpointNameMetadata(EndpointNames.AddPatron));

    }
}
