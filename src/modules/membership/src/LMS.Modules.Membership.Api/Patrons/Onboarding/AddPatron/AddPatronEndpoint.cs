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
            ICommandDispatcher dispatcher) =>
        {
            AddPatronCommand command = request.ToCommand();

            Result<Response> addPatronResult = await dispatcher
            .DispatchAsync<AddPatronCommand, Result<Response>>(command, default);

            return addPatronResult.Match(Results.Ok, ProblemFactory.Create);
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership);

    }
}
