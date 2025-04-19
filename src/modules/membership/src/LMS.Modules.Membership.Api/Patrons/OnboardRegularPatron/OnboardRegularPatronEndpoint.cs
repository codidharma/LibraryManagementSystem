
using LMS.Common.Application.Dispatchers;
using LMS.Modules.Membership.Application.Patrons.OnboardRegularPatron;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;


namespace LMS.Modules.Membership.Api.Patrons.OnboardRegularPatron;

internal sealed class OnboardRegularPatronEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("membership/onboarding/regularpatron", async (Request request, ICommandDispatcher dispatcher) =>
        {
            Application.Patrons.OnboardRegularPatron.Address address = new
                 (
                     BuildingNumber: request.Address.BuildingNumber,
                     StreetName: request.Address.StreetName,
                     City: request.Address.City,
                     State: request.Address.State,
                     Country: request.Address.Country,
                     ZipCode: request.Address.ZipCode
                 );
            List<Application.Patrons.OnboardRegularPatron.Document> identityDocuments = request
            .IdentityDocuments
            .Select(
                d => new Application.Patrons.OnboardRegularPatron.Document(
                    DocumentType: d.DocumentType,
                    ContentType: d.ContentType,
                    Content: d.Content)).ToList();


            OnboardRegularPatronCommand command = new(
                Name: request.Name,
                Email: request.Email,
                Gender: request.Gender,
                DateOfBirth: request.DateOfBirth,
                Address: address,
                IdentityDocuments: identityDocuments);

            Guid id = await dispatcher.DispatchAsync<OnboardRegularPatronCommand, Guid>(command, default);

            Response response = new(id);

            return TypedResults.Created<Response>(uri: string.Empty, response);
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership);
    }
}
