using LMS.Common.Domain;
using Microsoft.AspNetCore.Http;

namespace LMS.Common.Api.Results;

public static class ProblemFactory
{
    public static IResult Create(Result result)
    {
        if (result.IsSuccess)
        {
            //Do nthing
        }
        return Microsoft.AspNetCore.Http.Results.Problem(
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            statusCode: GetStatusCode(result.Error),
            type: GetType(result.Error),
            extensions: GetExtensions(result)
            );

        static string GetTitle(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Internal Server Error"
            };
        static string GetDetail(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Conflict => error.Description,
                _ => "An unexpected error occured. Please contact system administrator."
            };

        static int GetStatusCode(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static string GetType(Error error) =>
            error.ErrorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
        static Dictionary<string, object?>? GetExtensions(Result result)
        {
            if (result.Error is not ValidationError ve)
            {
                return null;
            }
            return new Dictionary<string, object?>
            {
                { "errors", ve.Errors}
            };
        }
    }
}
