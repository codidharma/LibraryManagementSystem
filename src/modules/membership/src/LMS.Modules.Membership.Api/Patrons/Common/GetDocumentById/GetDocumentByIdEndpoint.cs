using LMS.Common.Api.Results;
using LMS.Common.Application.Dispatchers.Query;
using LMS.Common.Domain;
using LMS.Modules.Membership.Application.Patrons.Onboarding.GetDocumentById;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace LMS.Modules.Membership.Api.Patrons.Common.GetDocumentById;

internal sealed class GetDocumentByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/memberships/patrons/{patronId}/documents/{documentId}", async (
            Guid patronId,
            Guid documentId,
            HttpContext httpContext,
            IQueryDispatcher dispatcher
            ) =>
        {
            GetDocumentByIdQuery query = new(patronId, documentId);

            Result<QueryResponse> result = await dispatcher.DispatchAsync<GetDocumentByIdQuery, Result<QueryResponse>>(query, default);

            if (result.IsSuccess)
            {
                byte[] fileContent = Convert.FromBase64String(result.Value.Content);
                string fileName = result.Value.FileName;
                string contentType = "application/octet-stream";

                return TypedResults.File(fileContent, contentType, fileName);
            }
            return ProblemFactory.Create(result);
        })
            .WithName(EndpointNames.GetDocumentById)
            .WithTags(Tags.Membership)
            .WithDescription("Get document using document id for the patron.")
            .WithDisplayName(EndpointNames.GetDocumentById);
    }
}
