using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Query;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentsListByPatronId;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.GetDocumentsListByPatronId;

internal sealed class GetDocumentsListByPatronIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/memberships/patrons/{id}/documents", async (
            Guid id,
            IQueryDispatcher dispatcher,
            HttpContext httpContext,
            LinkGenerator linkGenerator) =>
        {
            Result<QueryResponse> getResult = await dispatcher.DispatchAsync<Guid, Result<QueryResponse>>(id, default);

            if (getResult.IsSuccess)
            {
                List<DocumentInformation> documentInformations = [];

                foreach (DocumentMetadata metadata in getResult.Value.Documents)
                {

                    DocumentInformation information = new(
                        Name: metadata.Name,
                        DocumentLink: metadata.DocumentType,
                        Href: linkGenerator.GetUriByName(
                            httpContext,
                            EndpointNames.GetDocumentById, values: new
                            { patronId = id.ToString(), documentId = metadata.DocumentId.ToString() })!,
                        Rel: EndpointNames.GetDocumentById,
                        Method: HttpMethodConstants.Get);

                    documentInformations.Add(information);
                }

                Response response = new(documentInformations);
                return TypedResults.Ok(response);
            }
            return ProblemFactory.Create(getResult);

        }).WithName(EndpointNames.GetDocumentsListByPatronId);


    }
}
