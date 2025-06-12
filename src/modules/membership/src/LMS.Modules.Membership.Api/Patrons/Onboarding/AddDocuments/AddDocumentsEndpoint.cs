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
        app.MapPut("/memberships/onboarding/patrons/{id}/documents", async (
            Guid id,
            [FromForm] IFormFileCollection formFiles,
            ICommandDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator
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

            if (result.IsSuccess)
            {
                BaseResponse baseResponse = new()
                {
                    Links = [
                        new(
                            Href:linkGenerator.GetUriByName(httpContext, EndpointNames.GetDocumentsListByPatronId, values: new{ id = id.ToString()})!,
                            Rel: EndpointNames.GetDocumentsListByPatronId,
                            Method:HttpMethods.Get
                            )
                        ]
                };
                return TypedResults.Ok(baseResponse);
            }
            return ProblemFactory.Create(result);

        })
            .WithName(EndpointNames.AddDocuments)
            .AllowAnonymous()
            .WithTags(Tags.Membership)
            .DisableAntiforgery();

    }
}
