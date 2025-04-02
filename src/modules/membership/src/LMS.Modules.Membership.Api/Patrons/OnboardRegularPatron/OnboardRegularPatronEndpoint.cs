
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
            OnboardRegularPatronCommand command = new()
            {
                Name = request.Name,
                Email = request.Email,
                Gender = request.Gender,
                DateOfBirth = request.DateOfBirth,
                Address = new()
                {
                    StreetName = request.Address.StreetName,
                    City = request.Address.City,
                    State = request.Address.State,
                    Country = request.Address.Country,
                    ZipCode = request.Address.ZipCode
                },
                IdentityDocuments = request.IdentityDocuments.Select(d => new Application.Patrons.OnboardRegularPatron.Document()
                {
                    Content = d.Content,
                    ContentType = d.ContentType,
                    DocumentType = d.DocumentType
                }).ToList()
            };

            Guid id = await dispatcher.DispatchAsync<OnboardRegularPatronCommand, Guid>(command, default);

            return TypedResults.Created(id.ToString());
        })
        .AllowAnonymous()
        .WithTags(Tags.Membership);
    }
}
