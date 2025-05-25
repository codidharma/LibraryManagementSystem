using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddAddress;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddAddress;

internal sealed class AddAddressEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/membership/onboarding/patron/{id:guid}/address", async (
            [FromHeader(Name = "tracking-id")] string trackingId,
            [FromRoute] Guid id,
            [FromBody] Request request,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator) =>
        {
            AddAddressCommand command = request.ToCommand(id);

            Result addAddressResult = await dispatcher.DispatchAsync<AddAddressCommand, Result>(command, default);
            if (addAddressResult.IsSuccess)
            {
                List<HypermediaLink> links = [

                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNamesConstants.GetAddressByPatronId,
                        values: new { id = id.ToString()})!, "Self", HttpMethodConstants.Get),

                    new(linkGenerator.GetUriByName(
                        httpContext,
                        EndpointNamesConstants.AddDocuments,
                        values: new { id = id.ToString()})!, EndpointNamesConstants.AddDocuments, HttpMethodConstants.Put)

                    ];
                BaseResponse response = new() { Links = links };

                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(addAddressResult);
        })
            .WithName(EndpointNamesConstants.AddAddress)
            .WithTags(Tags.Membership);
    }
}
