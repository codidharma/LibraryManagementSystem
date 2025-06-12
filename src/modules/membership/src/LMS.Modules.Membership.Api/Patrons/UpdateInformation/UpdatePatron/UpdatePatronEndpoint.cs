using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.UpdateInformation.UpdatePatron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.UpdateInformation.UpdatePatron;

internal sealed class UpdatePatronEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("/memberships/updateinformation/patrons/{id}", async (
            Guid id,
            [FromBody] Request request,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator) =>
        {
            UpdatePatronCommand command = request.ToCommand(id);
            Result updateResult = await dispatcher.DispatchAsync<UpdatePatronCommand, Result>(command, default);

            if (updateResult.IsSuccess)
            {

                List<HypermediaLink> links = [

                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNames.GetPatronById,
                        values: new{id = id.ToString() })!, "self", HttpMethodConstants.Get),
                    ];
                BaseResponse response = new()
                {
                    Links = links
                };

                return TypedResults.Ok(response);
            }

            return ProblemFactory.Create(updateResult);
        })
            .WithName(EndpointNames.UpdatePatron);
    }
}
