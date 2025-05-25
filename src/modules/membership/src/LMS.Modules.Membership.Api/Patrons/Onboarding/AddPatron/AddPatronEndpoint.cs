using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddPatron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddPatron;

internal sealed class AddPatronEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/membership/onboarding/patron", async (
            Request request,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGerneator) =>
        {
            AddPatronCommand command = request.ToCommand();

            Result<CommandResult> addPatronResult = await dispatcher
            .DispatchAsync<AddPatronCommand, Result<CommandResult>>(command, default);

            if (addPatronResult.IsSuccess)
            {
                Response response = addPatronResult.Value.ToDto();

                List<HypermediaLink> links = [

                    new(linkGerneator.GetUriByName(
                        httpContext,
                        EndpointNamesConstants.GetPatronById,
                        values: new{id = response.Id.ToString() })!, "self", HttpMethodConstants.Get),
                    new(linkGerneator.GetUriByName(
                        httpContext,
                        EndpointNamesConstants.AddAddress,
                        values: new{id = response.Id.ToString() })!, EndpointNamesConstants.AddAddress, HttpMethodConstants.Put)

                    ];

                response.Links = links;

                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(addPatronResult);
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership)
        .WithName(EndpointNamesConstants.AddPatron)
        .WithMetadata(new EndpointNameMetadata(EndpointNamesConstants.AddPatron));

    }
}
