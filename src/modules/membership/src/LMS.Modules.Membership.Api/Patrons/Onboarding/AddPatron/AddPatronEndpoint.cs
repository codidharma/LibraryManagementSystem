using LMS.Common.Application.Dispatchers;
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
            ICommandDispatcher dispatcher) =>
        {
            AddPatronCommand command = request.ToCommand();
            Guid patronId = await dispatcher.DispatchAsync<AddPatronCommand, Guid>(command, default);
            Response response = new(patronId);

            return TypedResults.Created(uri: string.Empty, response);
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership);

    }
}
