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
        app.MapPatch("/membership/updateinformation/patrons/{id}", async (
            Guid id,
            [FromBody] Request request,
            ICommandDispatcher dispatcher) =>
        {
            UpdatePatronCommand command = request.ToCommand(id);
            Result updateResult = await dispatcher.DispatchAsync<UpdatePatronCommand, Result>(command, default);

            return updateResult.Match(Results.NoContent, ProblemFactory.Create);
        })
            .WithName("UpdatePatron");
    }
}
