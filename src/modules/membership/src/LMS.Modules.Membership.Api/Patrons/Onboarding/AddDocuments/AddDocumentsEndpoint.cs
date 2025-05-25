using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Command;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddDocuments;

internal sealed class AddDocumentsEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("/membership/onboarding/patron/{id}/documents", async (
            [FromRoute] Guid id,
            [FromForm] IFormFileCollection formFiles,
            ICommandDispatcher dispatcher
            ) =>
        {
            List<Document> documents = [];
            foreach (IFormFile formFile in formFiles)
            {

                Document document = await formFile.ToDocumentAsync();
                documents.Add(document);

            }
            AddDocumentsCommand command = new(id, documents);

            Result result = await dispatcher.DispatchAsync<AddDocumentsCommand, Result>(command, default);
            return result.Match(Results.NoContent, ProblemFactory.Create);

        })
            .WithName(EndpointNamesConstants.AddDocuments)
            .AllowAnonymous()
            .WithTags(Tags.Membership)
            .DisableAntiforgery();

    }
}
