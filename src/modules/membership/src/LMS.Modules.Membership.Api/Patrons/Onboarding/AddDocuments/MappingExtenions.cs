using LMS.Modules.Membership.Application.Patrons.Onboarding.AddDocuments;
using Microsoft.AspNetCore.Http;

namespace LMS.Modules.Membership.Api.Patrons.Onboarding.AddDocuments;

internal static class MappingExtenions
{
    public static async Task<Document> ToDocumentAsync(this IFormFile formFile)
    {
        using MemoryStream ms = new();
        await formFile.CopyToAsync(ms);
        string fileContents = Convert.ToBase64String(ms.ToArray());
        Document document = new(
            Name: formFile.FileName,
            DocumentType: formFile.Name,
            ContentType: formFile.ContentType,
            Content: fileContents);

        return document;
    }
}
